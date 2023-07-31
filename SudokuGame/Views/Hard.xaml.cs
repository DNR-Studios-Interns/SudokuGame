﻿using MediaManager;
using SudokuGame.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SudokuGame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Hard : ContentPage
    {
        DateTime startTime;
        Timer timer = new Timer();
        public Hard()
        {
            InitializeComponent();
            BindingContext = this;
            timer.Interval = 1000;
            timer.Elapsed += Timer_Tick;
        }

        char numChoice = '0';
        int rowChoice;
        int columnChoice;
        int countError = 0;
        int countCorrect = 0;
        bool valid;
        bool wonGame = false;
        int pencilCount = 0;
        bool pencil = false;


        //board solution
        string[] solution = new string[9] {"126437958",
                                           "895621473",
                                           "374985126",
                                           "457193862",
                                           "983246517",
                                           "612578394",
                                           "269314785",
                                           "548769231",
                                           "731852649"
                                           };

        void NumSelected1(object sender, EventArgs e)
        {
            numChoice = '1';
            Console.WriteLine(numChoice);
            NumSelectHard.Text = "Number selected: " + numChoice;

        }
        void NumSelected2(object sender, EventArgs e)
        {
            numChoice = '2';
            Console.WriteLine(numChoice);
            NumSelectHard.Text = "Number selected: " + numChoice;
        }
        void NumSelected3(object sender, EventArgs e)
        {
            numChoice = '3';
            Console.WriteLine(numChoice);
            NumSelectHard.Text = "Number selected: " + numChoice;
        }
        void NumSelected4(object sender, EventArgs e)
        {
            numChoice = '4';
            Console.WriteLine(numChoice);
            NumSelectHard.Text = "Number selected: " + numChoice;
        }
        void NumSelected5(object sender, EventArgs e)
        {
            numChoice = '5';
            Console.WriteLine(numChoice);
            NumSelectHard.Text = "Number selected: " + numChoice;
        }
        void NumSelected6(object sender, EventArgs e)
        {
            numChoice = '6';
            Console.WriteLine(numChoice);
            NumSelectHard.Text = "Number selected: " + numChoice;
        }
        void NumSelected7(object sender, EventArgs e)
        {
            numChoice = '7';
            Console.WriteLine(numChoice);
            NumSelectHard.Text = "Number selected: " + numChoice;
        }
        void NumSelected8(object sender, EventArgs e)
        {
            numChoice = '8';
            Console.WriteLine(numChoice);
            NumSelectHard.Text = "Number selected: " + numChoice;
        }
        void NumSelected9(object sender, EventArgs e)
        {
            numChoice = '9';
            Console.WriteLine(numChoice);
            NumSelectHard.Text = "Number selected: " + numChoice;
        }

        public void PencilTime(object sender, EventArgs e)
        {
            pencilCount++;
            switch (pencilCount % 2)
            {
                case 1:
                    pencil = true;
                    MyImageButton.Source = "pencil_select.png";
                    Console.WriteLine(pencil);
                    break;
                case 0:
                    pencil = false;
                    MyImageButton.Source = "pencil.png";
                    Console.WriteLine(pencil);
                    break;
                default:
                    break;
            }
        }


        public ICommand ButtonCommand => new Command<string>(CommandButtonClick);



        public void CommandButtonClick(string a)//Command
        {
            if (pencil == false && numChoice != '0')
            {
                char r = a[0];
                char c = a[1];
                rowChoice = (int)Char.GetNumericValue(r);
                columnChoice = (int)Char.GetNumericValue(c);
                char ans = solution[rowChoice][columnChoice];
                if (ans == numChoice) { valid = true; countCorrect++; Console.WriteLine(countCorrect); }
                else { valid = false; ErrorCounterHard.Text = "Errors: " + ++countError; }
            }
        }//end of Command

        async void isValid(object sender, EventArgs e)//isValid function
        {
            if (valid == true && pencil == false)
            {
                (sender as Button).Text = numChoice.ToString();
                Button clickedButton = (Button)sender;
                clickedButton.IsEnabled = false;
                switch (countCorrect % 6)
                {
                    case 0:
                        DependencyService.Get<IAudio>().PlayAudioFile("r-good-job.mp3");
                        break;
                    case 1:
                        DependencyService.Get<IAudio>().PlayAudioFile("d-youre-a-winner.mp3");
                        break;
                    case 2:
                        DependencyService.Get<IAudio>().PlayAudioFile("r-winner.mp3");
                        break;
                    case 3:
                        DependencyService.Get<IAudio>().PlayAudioFile("d-great-job.mp3");
                        break;
                    case 4:
                        DependencyService.Get<IAudio>().PlayAudioFile("r-yay-right.mp3");
                        break;
                    case 5:
                        DependencyService.Get<IAudio>().PlayAudioFile("d-chicken-dinner.mp3");
                        break;

                    default:
                        break;
                }
            }
            else if (valid == false && pencil == false)
            {
                (sender as Button).Text = "";
                switch (countError % 7)
                {
                    case 0:
                        DependencyService.Get<IAudio>().PlayAudioFile("d-try-again.mp3");
                        break;
                    case 1:
                        DependencyService.Get<IAudio>().PlayAudioFile("d-oh-that-was-terrible.mp3");
                        break;
                    case 2:
                        DependencyService.Get<IAudio>().PlayAudioFile("d-whoops.mp3");
                        break;
                    case 3:
                        DependencyService.Get<IAudio>().PlayAudioFile("r-you-suck.mp3");
                        break;
                    case 4:
                        DependencyService.Get<IAudio>().PlayAudioFile("r-uh-oh.mp3");
                        break;
                    case 5:
                        DependencyService.Get<IAudio>().PlayAudioFile("r-loser.mp3");
                        break;
                    case 6:
                        DependencyService.Get<IAudio>().PlayAudioFile("d-fu.mp3");
                        break;

                    default:
                        break;
                }

            }
            else if (pencil == true && numChoice!='0')
            {
                (sender as Button).TextColor = Color.Red;
                (sender as Button).Text = numChoice.ToString();
            }
            if (countCorrect == 62)
            {
                wonGame = true;
                Console.WriteLine(wonGame);
                await Navigation.PushAsync(new WinnerPage());
            }


        }//end of isValid

        //adding a timer here
        protected override void OnAppearing()
        {
            base.OnAppearing();
            startTime = DateTime.Now;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            Device.BeginInvokeOnMainThread(() =>
            {
                TimerCounterHard.Text = elapsed.ToString(@"hh\:mm\:ss");
            });
        }
    }
}