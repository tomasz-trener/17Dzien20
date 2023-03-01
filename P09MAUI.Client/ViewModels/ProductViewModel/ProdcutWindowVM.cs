using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P05Sklep.Shared;
using P09MAUI.Client.Tools;
using P09MAUI.Client.ViewModels.ProductViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P09MAUI.Client.ViewModels.ProductViewModel
{
    // klasa musi być partial bo paczka mvvm toolkit dodaje do naszej klasy automatycznie rozszerzenia 
    public partial class ProdcutWindowVM : ObservableObject
    {

        private ProductsApiTool productsApiTool; 

        public ObservableCollection<ProductVM> Products { get; set; }

        //private bool speaking = false;
        //public bool Speaking { get => speaking;
        //    set
        //    {
        //        speaking = value;
        //        OnPropertyChange();
        //    }
        //}

        //public DelegateCommand EditCommand { get; set; }
        //public DelegateCommand DeleteCommand { get; set; }     
        //public DelegateCommand CreateCommand { get; set; }
        //public DelegateCommand SpeakCommand { get; set; }

        [ObservableProperty]
        private ProductVM selectedProduct= new ProductVM();

        //private ProductVM selectedProduct = new ProductVM();

        //public ProductVM SelectedProduct
        //{
        //    get { return selectedProduct; }
        //    set
        //    {
        //        if(value != null)
        //            selectedProduct = value;

        //        OnPropertyChange();
        //    }
        //}

        public ProdcutWindowVM(ProductsApiTool productsApiTool)
        {
            this.productsApiTool = productsApiTool;

            Products = new ObservableCollection<ProductVM>();
            GetPoducts();
            

            //EditCommand = new DelegateCommand(EditProduct, null);
            //DeleteCommand = new DelegateCommand(DeleteProduct, null);
            //CreateCommand = new DelegateCommand(CreateProduct, null);
            //SpeakCommand = new DelegateCommand(SpeakProductDescription, null);
        }

        //private async void SpeakProductDescription()
        //{
        //    Speaking = true;
        //    SpeechServiceTool sst = new SpeechServiceTool();
        //    string recognizeText = await sst.RecognizeAsync();

        //    selectedProduct.Description = recognizeText;
        //    OnPropertyChange("selectedProduct");
        //    Speaking = false;
        //}

        [RelayCommand]
        public async void Create()
        {
         //   ProductsApiTool productsApiTool = new ProductsApiTool();

            var productToCreate = new Product()
            {
              //  Id = selectedProduct.Id,
                Color = selectedProduct.Color,
                Description = selectedProduct.Description,
                Title = selectedProduct.Title,
                ImageUrl = selectedProduct.ImageUrl
            };
            await productsApiTool.CreateProduct(productToCreate);
            GetPoducts();
        }

        [RelayCommand]
        public async void Delete()
        {
        //    ProductsApiTool productsApiTool = new ProductsApiTool();

            await productsApiTool.DeleteProduct(selectedProduct.Id);
            
            GetPoducts();
            selectedProduct = new ProductVM();
        }

        [RelayCommand]
        public async void Edit()
        {
         //   ProductsApiTool productsApiTool = new ProductsApiTool();

            var productToUpdate = new Product()
            {
                Id = selectedProduct.Id,
                Color = selectedProduct.Color,
                Description = selectedProduct.Description,
                Title = selectedProduct.Title,
                ImageUrl = selectedProduct.ImageUrl
            };
            await productsApiTool.UpdateProduct(productToUpdate);
            GetPoducts();
        }

        private async void GetPoducts()
        {
      //      ProductsApiTool productsApiTool = new ProductsApiTool();
            var products = await productsApiTool.GetProducts();

            Products.Clear();
            foreach (var p in products)
            {
                Products.Add(new ProductVM(p));
            }
        }
    }
}
