﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace StopwatchTimer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();

        Stopwatch watch = new Stopwatch();

        string currentTime = string.Empty;

        string currentSecs = string.Empty;

        string dateSecond = string.Empty;

        DateTime date = DateTime.Now;

        DateTime now;

        int lapCount = 0;

        public MainPage()
        {
            this.InitializeComponent();
            timer.Tick += new EventHandler<object>(dt_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        //print times and handle progress bar

        private void dt_Tick(object sender, object e)
        {
            if(watch.IsRunning)
            {
                TimeSpan time = watch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}", time.Hours, time.Minutes, time.Seconds, time.Milliseconds / 10);
                timertxtblock.Text = currentTime;

                currentSecs = String.Format("{0:00}", time.Seconds);

                timeinsecs.Text = currentSecs;

                now = date.Add(time);

                datetxtblock.Text = now.ToString();

                if (bar.Value >= 0 && bar.Value < 59.999)
                {
                    bar.Foreground = new SolidColorBrush(Colors.LightSkyBlue);
                    bar.Value = Convert.ToDouble(bar.Value) + 0.033171;
                }
                else
                {
                    bar.Value = 0;
                }

                if (bar2.Value >= 0 && bar2.Value < 59.999)
                {
                    bar2.Foreground = new SolidColorBrush(Colors.LightGreen);
                    bar2.Value = Convert.ToDouble(bar2.Value) + 0.00055285;
                }
                else
                {
                    bar2.Value = 0;
                }



            }
        }

        //start watch

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            watch.Start();
            timer.Start();
        }

        //stop watch

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            if(watch.IsRunning)
            {
                watch.Stop();
            }
            
        }

        //reset watch

        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            watch.Reset();
            timertxtblock.Text = "00:00:00:00";
            bar.Value = 0;
            bar2.Value = 0;
            timeinsecs.Text = "00";
            lapCount = 0;
            laps.Text = "";
        }

        //print lap times

        private void Lap_Button_Click(object sender, RoutedEventArgs e)
        {
           if(watch.IsRunning)
            {
                lapCount++;
                
                laps.FontSize = 16;
                laps.FontWeight = FontWeights.Bold;
                laps.FontStyle = FontStyle.Italic;

                laps.Text += " Lap " + lapCount + " - Time - " + currentTime + Environment.NewLine + Environment.NewLine;


            }
           
        }
    }
}
