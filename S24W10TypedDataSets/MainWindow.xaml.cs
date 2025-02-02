﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace S24W10TypedDataSets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // table adapter
        NorthwindDataSetTableAdapters.ProductsTableAdapter adpProds = new NorthwindDataSetTableAdapters.ProductsTableAdapter();
        NorthwindDataSetTableAdapters.CategoriesTableAdapter adpCats = new NorthwindDataSetTableAdapters.CategoriesTableAdapter();
        NorthwindDataSetTableAdapters.ProductsWithCategoriesTableAdapter adpProdsCats = new NorthwindDataSetTableAdapters.ProductsWithCategoriesTableAdapter();

        // data table
        NorthwindDataSet.ProductsDataTable tblProds = new NorthwindDataSet.ProductsDataTable();
        NorthwindDataSet.CategoriesDataTable tblCats = new NorthwindDataSet.CategoriesDataTable();
        NorthwindDataSet.ProductsWithCategoriesDataTable tblProdsCats = new NorthwindDataSet.ProductsWithCategoriesDataTable();

        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            // Fill method
            //adpProds.Fill(tblProds);

            // Get method
            tblProds = adpProds.GetProducts();

            grdProducts.ItemsSource = tblProds;
        }

        private void btnLoadAllProducts_Click(object sender, RoutedEventArgs e)
        {
            LoadProducts();
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var row = tblProds.FindByProductID(id);

            if (row != null)
            {
                txtName.Text = row.ProductName;
                txtPrice.Text = row.UnitPrice.ToString();
                txtQuantity.Text = row.UnitsInStock.ToString();
            }
            else
            {
                txtName.Text = txtPrice.Text = txtQuantity.Text = "";
                MessageBox.Show("Invalid ID. Please try again.");
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            short quantity = short.Parse(txtQuantity.Text);

            adpProds.Insert(name, price, quantity);

            LoadProducts();
            MessageBox.Show("New product added");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);
            string name = txtName.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            short quantity = short.Parse(txtQuantity.Text);

            adpProds.Update(name, price, quantity, id);

            LoadProducts();
            MessageBox.Show("Product updated");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);
            
            adpProds.Delete(id);

            LoadProducts();
            MessageBox.Show("Product deleted");
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;

            tblProds = adpProds.GeProductByName(name);
            grdProducts.ItemsSource = tblProds;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tblCats = adpCats.GetCategories();

            cmbCategories.ItemsSource = tblCats;
            cmbCategories.DisplayMemberPath = "CategoryName";
            cmbCategories.SelectedValuePath = "CategoryID";
        }

        private void cmbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int catId = (int)cmbCategories.SelectedValue;

            tblProdsCats = adpProdsCats.GetProductsByCatId(catId);
            grdProducts.ItemsSource = tblProdsCats;
        }

        private void btnClearData_Click(object sender, RoutedEventArgs e)
        {
            cmbCategories.ItemsSource = null;
        }
    }
}