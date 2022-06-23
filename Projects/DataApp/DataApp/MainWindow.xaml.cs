using System;
using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;

namespace DataApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<PropertyNode> nodes;
        

        public MainWindow()
        {
            
            InitializeComponent();

            /*nodes = new ObservableCollection<PropertyNode>
            {
                new PropertyNode
                {
                    Name = "Имущественное",
                    Nodes = new ObservableCollection<PropertyNode>
                    {
                        new PropertyNode { Name = "Мебель"},
                        new PropertyNode { Name = "Компьютерная техника" },
                        new PropertyNode { Name = "Учебная техника" }
                    }
                },
                new PropertyNode
                {
                    Name = "Финансовое",
                    Nodes = new ObservableCollection<PropertyNode>
                    {
                        new PropertyNode { Name = "Кредит" },
                        new PropertyNode {Name="Задолженность перед банком" }
                    }
                }
            };
            treeView1.ItemsSource = nodes;
            */
            ObservableCollection<Company> companies = new ObservableCollection<Company>()
            {
                new Company
                {
                    Title="ОАО \"Столы блин-блинский\"",
                    Props = new ObservableCollection<Property>
                    {
                        new Property
                        {
                            ID="P01", Name="Стол", Price=5000
                        },
                        new Property
                        {
                            ID="P02", Name="Стул", Price=2000
                        },
                        new Property
                        {
                            ID="P04", Name="Говнео за 100 рублей", Price=100
                        },
                        new Property
                        {
                            ID="P06", Name="Не пиво", Price=210
                        }
                    }
                },
                new Company
                {
                    Title = "LG",
                    Props = new ObservableCollection<Property>
                    {
                        new Property
                        {
                            ID="P03", Name="Телевизор LG10500", Price=25000
                        }
                    }
                },
                new Company
                {
                    Title = "Mercedes",
                    Props = new ObservableCollection<Property>
                    {
                        new Property
                        {
                            ID="P05", Name="Машина для директора", Price=3425000
                        }
                    }
                }
            };

            mainMenu.ItemsSource = companies;

        }


    }

    public class PropertyRepository
    {
        private ObservableCollection<Property> props;
        
        

        public PropertyRepository()
        {
            props = new ObservableCollection<Property>
            {
            new Property
                {
                    ID="P01", Name="Стол", Price=5000, Company = "ОАО \"Столы блин-блинский\""
                },
                new Property
                {
                    ID="P02", Name="Стул", Price=2000, Company = "ООО \"Столы Петровича\""
                },
                new Property
                {
                    ID="P03", Name="Телевизор LG10500", Price=25000, Company =  "LG"
                },
                new Property
                {
                    ID="P04", Name="Говнео за 100 рублей", Price=100, Company =  "Говно"
                },
                new Property
                {
                    ID="P05", Name="Машина для директора", Price=3425000, Company =  "Mercedes"
                },
                new Property
                {
                    ID="P06", Name="Ne Pivo", Price=210, Company =  "ЗАО \"Не пивоварный завод\""
                },
            };
            
        }
            public ObservableCollection<Property> GetPropertys()
        {
            return props;
        }
    }
    public class Property
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Company { get; set; }
    }

    public class PropertyNode
    {
        public string Name { get; set; }
        public ObservableCollection<PropertyNode> Nodes { get; set; }
    }

    public class Company
    {
        public string Title { get; set; }
        public ObservableCollection<Property> Props { get; set; }

        public Company()
        {
            Props = new ObservableCollection<Property>();
        }
    }

}
