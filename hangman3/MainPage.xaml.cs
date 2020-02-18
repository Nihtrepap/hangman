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

        public MainPage()
        {
            this.InitializeComponent();
            theWord = "hangman".ToLower();
            count = theWord.Length;
            showLetter = new char[count];
            word = new StringBuilder(count);



            LoadStringBuild();
            LoadOpacity();
        }
        private void Test_Click(object sender, RoutedEventArgs e)
        {
            GetLines();

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            countClick = 0;
            LoadOpacity();
        }

        private void Guess_Click(object sender, RoutedEventArgs e)
        {
            CheckLetter();

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
            if (countClick == 1) printBody.Opacity = 1;
            else if (countClick == 2) printLeftArm.Opacity = 1;
            else if (countClick == 3) printRightArm.Opacity = 1;
            else if (countClick == 4) printLeftLeg.Opacity = 1;
            else if (countClick == 5) { printRightLeg.Opacity = 1; txtGameOver.Text = "YOU JUST KILLED THE STICKMAN\n=("; }
        }/// <summary>
         /// Checks if letter exists in word. If not it will it will call GetLines()
         /// 
         /// <see cref="GetLines()"/>
         /// </summary>
        public void CheckLetter()
        {
            if (theWord.Contains(txtInputGuess.Text.ToLower()))
            {
                for (int i = 0; i < theWord.Length; i++)
                {
                    if (txtInputGuess.Text.ToLower().ToString() == theWord[i].ToString())
                    {
                        word.Remove(i, 1);
                        word.Insert(i, txtInputGuess.Text.ToLower());
                    }
                    else if(word.ToString() == theWord)
                    {
                        txtGameOver.Text = "YOU WON THE GAME AND SAVED THE STICKMAN! \n=)";
                    }
                    txtRightGuess.Text = word.ToString();
                }
            }
            else if (!theWord.Contains(txtInputGuess.Text))
            {
                GetLines();
            }

        }
        public void LoadStringBuild()
        {
            for (int i = 0; i < count; i++)
            {
                if (word.ToString().Length != count)
                    word.Insert(i, '_');
            }
        }
    }
}

