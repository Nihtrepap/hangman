using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace hangman3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public int countClick = 0;
        public string theWord;
        public char[] showLetter;
        public int count;
        public StringBuilder word;
        public bool playing;
        public bool chosen;

        public MainPage()
        {
            this.InitializeComponent();
            
            theWord = "X";
            word = new StringBuilder(theWord.Length);

        }
       
        

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            playing = false;
            countClick = 0;
            txtGameOver.Text = "";
            txtRightGuess.Text = "";
            txtWrongGuessed.Text = "";
            word.Clear();
            //LoadStringBuild();
            LoadOpacity();
            RemoveFace();
        }

        private void Guess_Click(object sender, RoutedEventArgs e)
        {
            if (txtInputGuess.Text.Length == 1) CheckLetter();
            else if(playing) txtGameOver.Text = $"Only use ONE char not {txtInputGuess.Text.Length}";
            txtInputGuess.Text = "";
        }

        
        /// <summary>
        /// This method make the lines dissappear using opacity
        /// </summary>
        public void LoadOpacity()
        {
            printBody.Opacity = 0;
            printHead.Opacity = 0;
            printLeftArm.Opacity = 0;
            printRightArm.Opacity = 0;
            printLeftLeg.Opacity = 0;
            printRightLeg.Opacity = 0;
        }
        /// <summary>
        /// This method prints out the lines of the hangman.
        /// One line for each wrong guess.
        /// </summary>
        public void GetLines()
        {
            countClick++;
            if (countClick == 1) { printHead.Opacity = 1; GetFace(); }
            else if (countClick == 2) printBody.Opacity = 1;
            else if (countClick == 3) printLeftArm.Opacity = 1;
            else if (countClick == 4) printRightArm.Opacity = 1;
            else if (countClick == 5) printLeftLeg.Opacity = 1;
            else if (countClick == 6) { printRightLeg.Opacity = 1; theWord = "X"; txtGameOver.Text = "YOU JUST KILLED THE STICKMAN\n=("; playing = false; }
        }
        public void GetFace()
        {
            eyeFour.Opacity = 1;
            eyeThree.Opacity = 1;
            eyeTwo.Opacity = 1;
            eyeOne.Opacity = 1;
            mouth.Opacity = 1;
        }
        public void RemoveFace()
        {
            eyeFour.Opacity = 0;
            eyeThree.Opacity = 0;
            eyeTwo.Opacity = 0;
            eyeOne.Opacity = 0;
            mouth.Opacity = 0;
        }
         /// <summary>
         /// Checks if letter exists in word. If not it will it will call GetLines()
         /// Will also check if char is guessed already.
         /// 
         /// <see cref="GetLines()"/>
         /// </summary>
        public void CheckLetter()
        {
            if (theWord.Contains(txtInputGuess.Text.ToLower()) && theWord != "X")
            {
                playing = true;
                txtGameOver.Text = "";
                for (int i = 0; i < theWord.Length; i++)
                {
                    if (txtInputGuess.Text.ToLower().ToString() == theWord[i].ToString())
                    {
                        word.Remove(i, 1);
                        word.Insert(i, txtInputGuess.Text.ToLower());
                    }
                    if(word.ToString() == theWord)
                    {
                        txtGameOver.Text = "YOU WON THE GAME AND SAVED THE STICKMAN! \n=)";
                        theWord = "X";
                    }
                    txtRightGuess.Text = word.ToString();
                }
            }
            else if (!theWord.Contains(txtInputGuess.Text) && theWord != "X")
            {
                if (!txtWrongGuessed.Text.Contains(txtInputGuess.Text)){ GetLines(); AddWrongLetter(); }
                else txtGameOver.Text = "Already guessed that char";
            }
            else
            {
                txtGameOver.Text = "Please choose word before starting";
            }

        }
        public void AddWrongLetter()
        {
            txtWrongGuessed.Text += $"{txtInputGuess.Text}, ";
        }
        /// <summary>
        /// This builds up the stringbuild.
        /// If my stringbuild is not as long as theWord, then input _
        /// </summary>
            public void LoadStringBuild()
        {
            for (int i = 0; i < theWord.Length; i++)
            {
                if (word.ToString().Length != theWord.Length)
                    word.Insert(i, '_');
            }
        }
        /// <summary>
        /// This will only work if the word is not chosen. 
        /// </summary>
        public void NewGuessWord()
        {
            if (playing == false)
            {
                theWord = txtNewWord.Text.ToLower();
                playing = true; 
                LoadStringBuild();
                txtNewWord.Text = "";
            }
            else txtGameOver.Text = "Cant change word while in game";
        }

        private void NewWord_Click(object sender, RoutedEventArgs e)
        {
            NewGuessWord();
        }
    }
}

