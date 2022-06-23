using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace WpfDiplomApp
{
    /// <summary>
    /// Логика взаимодействия для Analysis.xaml
    /// </summary>
    public partial class Analysis : Window
    {
        public Func<ChartPoint, string> PointLabel { get; set; }
        public List<PropCount> propsCountList = new List<PropCount>();
        public List<Property> PropsList { get; set; }
        public Analysis(List<Property> propsList)
        {
            
            PointLabel = chartPoint => string.Format("{0}({1:p})", chartPoint.Y+100, chartPoint.Participation);
            DataContext = this;
            InitializeList(propsList);
            InitializeComponent();
            pipChart.Series = PieFillCollection(propsCountList);
            AddingCountText(propsList);
        }

        private void pipChart_DataClick(object sender, LiveCharts.ChartPoint chartPoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartPoint.ChartView;


            //clear selected slice
            foreach (PieSeries series in chart.Series)
            {
                series.PushOut = 0;

                var selectedSeries = (PieSeries)chartPoint.SeriesView;
                selectedSeries.PushOut = 8;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private SeriesCollection PieFillCollection(List<PropCount> propsCountList)
        {
            SeriesCollection sc = new SeriesCollection();
            foreach (PropCount tempPropCount in propsCountList)
            {
                sc.Add(new PieSeries { Title = tempPropCount.PropName, Values = new ChartValues<int> { tempPropCount.Count }, DataLabels = false, LabelPoint = PointLabel });
            }
            return sc;
        }
        private void InitializeList(List<Property> propsList)
        {
            PropsList = propsList;
            try
            {
                string type = PropsList[0].ObjectType;
                propsCountList.Add(new PropCount { PropName = type, Count = 0 });
                bool find;
                PropCount propCount = new PropCount();
                foreach (Property prop in PropsList)
                {
                    find = false;
                    type = prop.ObjectType;
                    for (int i = 0; i < propsCountList.Count; i++)
                    {
                        propCount = propsCountList[i];
                        if (type == propCount.PropName)
                        {
                            find = true;
                            propCount.Count++;
                        }
                    }
                    if (!find)
                        propsCountList.Add(new PropCount { PropName = type, Count = 1 });
                }
            }
            catch
            {
                MessageBox.Show("Данные не обнаружены");
            }

        }

        private void AddingCountText(List<Property> propsList)
        {
            int sum = 0;
            foreach(Property tempProp in propsList)
            {
                sum += tempProp.Price;
             }
            TextBlock textBlockCount = new TextBlock();
            textBlockCount.Text ="Всего было потрачено денег: " + sum.ToString() + " руб";
            textBlockCount.Margin = new Thickness(2);
            attPool.Children.Add(textBlockCount);

            textBlockCount = new TextBlock();
            textBlockCount.Text = "Средняя стоимость объекта учета: " + (sum/propsList.Count).ToString() + " руб";
            textBlockCount.Margin = new Thickness(2);
            attPool.Children.Add(textBlockCount);
        }
    }

        public class PropCount
        {
            private int count;
            public string PropName { get; set; }
            public int Count {
                get
                {
                    return count;
                }
                set
                {
                    count = value;
                }
            }
        }
}
