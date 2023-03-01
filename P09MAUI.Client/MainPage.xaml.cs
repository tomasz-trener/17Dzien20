using P09MAUI.Client.ViewModels.ProductViewModel;

namespace P09MAUI.Client
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage(ProdcutWindowVM viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

       
    }
}