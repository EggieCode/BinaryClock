using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Runtime.Serialization.Formatters;
using System.Data.Linq;
using System.Collections;


namespace BinaryClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Timer timer;
        public MainWindow()
        {
            InitializeComponent();
            //Zorgt er voor om de dat het verster via de led's kan verplaatsen
            this.MouseDown += title_MouseLeftButtonDown;

            //Zet hier de timer op en start die
            timer = new Timer(20);
            TimerEvent(timer, new EventArgs());

            //Zet de timer vast aan het event.
            timer.Elapsed += TimerEvent;

            timer.Start();
        }

        //Event voor de timer, om elke 20msec te controleren
        private void TimerEvent(object sender, EventArgs e)
        {
            //Gespliste nummers in een list
            List<int> Listhour = new List<int>();
            List<int> Listmin = new List<int>();
            List<int> Listsec = new List<int>();
            //Haalt de tijd op en zet in de Lijsten (Listhour, listmin, Listsec)
            DateTime.Now.Hour.ToString().ToCharArray().ToList().ForEach(v => Listhour.Add(int.Parse(v.ToString())));
            DateTime.Now.Minute.ToString().ToCharArray().ToList().ForEach(v => Listmin.Add(int.Parse(v.ToString())));
            DateTime.Now.Second.ToString().ToCharArray().ToList().ForEach(v => Listsec.Add(int.Parse(v.ToString())));

            //Fix: Als je zit niet dan crashed de applicatie omdat je niet van uit de Timer het uiterlijk kan aanpassen.
            Dispatcher.BeginInvoke(new Action(() =>
            {
                //Set eerst alle led's naar grijs
                foreach (object ell in mainGrid.Children)
                {
                    if (ell is Ellipse)
                        SetEllipseColor((Ellipse)ell, "#cccccc");
                }
                //Kijkt of het list meer dan 2 int bevat, anders zet hij de uur neer als minuten(Dit is ook bij de minuten en secodes)
                if (Listhour.Count == 2)
                {
                    //Kijk of de bit 1 true is dat zet hij de kleur (En gaat zo verder als 1,2,4,8)
                    if ((Listhour[1] & 0x01) > 0)
                        SetEllipseColor(Hour0, "#00ffff");
                    if ((Listhour[1] & 0x02) > 0)
                        SetEllipseColor(Hour1, "#00ffff");
                    if ((Listhour[1] & 0x04) > 0)
                        SetEllipseColor(Hour2, "#00ffff");
                    if ((Listhour[1] & 0x08) > 0)
                        SetEllipseColor(Hour3, "#00ffff");

                    if ((Listhour[0] & 0x01) > 0)
                        SetEllipseColor(Hour4, "#00ffff");
                    if ((Listhour[0] & 0x02) > 0)
                        SetEllipseColor(Hour5, "#00ffff");
                }
                else
                {
                    if ((Listhour[0] & 0x01) > 0)
                        SetEllipseColor(Hour0, "#00ffff");
                    if ((Listhour[0] & 0x02) > 0)
                        SetEllipseColor(Hour1, "#00ffff");
                    if ((Listhour[0] & 0x04) > 0)
                        SetEllipseColor(Hour2, "#00ffff");
                    if ((Listhour[0] & 0x08) > 0)
                        SetEllipseColor(Hour3, "#00ffff");
                }


                if (Listmin.Count == 2)
                {
                    if ((Listmin[1] & 0x01) > 0)
                        SetEllipseColor(Min0, "#00ffff");
                    if ((Listmin[1] & 0x02) > 0)
                        SetEllipseColor(Min1, "#00ffff");
                    if ((Listmin[1] & 0x04) > 0)
                        SetEllipseColor(Min2, "#00ffff");
                    if ((Listmin[1] & 0x08) > 0)
                        SetEllipseColor(Min3, "#00ffff");

                    if ((Listmin[0] & 0x01) > 0)
                        SetEllipseColor(Min4, "#00ffff");
                    if ((Listmin[0] & 0x02) > 0)
                        SetEllipseColor(Min5, "#00ffff");
                    if ((Listmin[0] & 0x04) > 0)
                        SetEllipseColor(Min6, "#00ffff");
                }
                else
                {
                    if ((Listmin[0] & 0x01) > 0)
                        SetEllipseColor(Min0, "#00ffff");
                    if ((Listmin[0] & 0x02) > 0)
                        SetEllipseColor(Min1, "#00ffff");
                    if ((Listmin[0] & 0x04) > 0)
                        SetEllipseColor(Min2, "#00ffff");
                    if ((Listmin[0] & 0x08) > 0)
                        SetEllipseColor(Min3, "#00ffff");
                }
                if (Listsec.Count == 2)
                {
                    if ((Listsec[1] & 0x01) > 0)
                        SetEllipseColor(Sec0, "#00ffff");
                    if ((Listsec[1] & 0x02) > 0)
                        SetEllipseColor(Sec1, "#00ffff");
                    if ((Listsec[1] & 0x04) > 0)
                        SetEllipseColor(Sec2, "#00ffff");
                    if ((Listsec[1] & 0x08) > 0)
                        SetEllipseColor(Sec3, "#00ffff");

                    if ((Listsec[0] & 0x01) > 0)
                        SetEllipseColor(Sec4, "#00ffff");
                    if ((Listsec[0] & 0x02) > 0)
                        SetEllipseColor(Sec5, "#00ffff");
                    if ((Listsec[0] & 0x04) > 0)
                        SetEllipseColor(Sec6, "#00ffff");
                }
                else
                {
                    if ((Listsec[0] & 0x01) > 0)
                        SetEllipseColor(Sec0, "#00ffff");
                    if ((Listsec[0] & 0x02) > 0)
                        SetEllipseColor(Sec1, "#00ffff");
                    if ((Listsec[0] & 0x04) > 0)
                        SetEllipseColor(Sec2, "#00ffff");
                    if ((Listsec[0] & 0x08) > 0)
                        SetEllipseColor(Sec3, "#00ffff");
                }

            }));
        }

        //Function voor de te verander van de Led's.
        void SetEllipseColor(Ellipse ell, string color)
        {

            ell.Fill = new SolidColorBrush()
            {
                Color = (Color)ColorConverter.ConvertFromString(color)
            };
        }
        // Zocht er voor als je de led's versleept versleep je het hele venster
        private void title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
}
