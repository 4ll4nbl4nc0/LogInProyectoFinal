using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LogInProyectoFinal.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogIn : ContentPage
    {
        UsuarioRepository loginRepository = new UsuarioRepository();
        public LogIn()
        {
            InitializeComponent();
            var passwordEntry = new Entry
            {
                Placeholder = "Contraseña",
                IsPassword = true
            };

            NavigationPage.SetHasNavigationBar(this, false); // Oculta la barra de navegación si es necesario
            var navigationPage = new NavigationPage(this);
            NavigationPage.SetHasBackButton(this, false); // Si deseas ocultar el botón de retroceso
            Application.Current.MainPage = navigationPage;
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            string correoElectronico = txtCorreoElectronico.Text;
            string contraseña = txtContraseña.Text;

            // Verificar si el correo electrónico o la contraseña están vacíos.
            if (string.IsNullOrEmpty(correoElectronico) || string.IsNullOrEmpty(contraseña))
            {
                await DisplayAlert("Alerta", "Ingrese su usuario y contraseña", "Ok");
                return;
            }

            try
            {
                // Obtener el usuario por el correo electrónico ingresado.
                var user = await loginRepository.GetUserByEmail(correoElectronico);

                // Aquí se asume que GetUserByEmail devuelve null si no encuentra un usuario.
                // Además, se agrega la comparación de la contraseña.
                if (user != null && user.Contraseña == contraseña)
                {
                    // Autenticación exitosa.
                    await DisplayAlert("Éxito", "Inicio de sesión exitoso", "Ok");

                    // Almacenar el correo electrónico del usuario en las propiedades de la aplicación
                    Application.Current.Properties["UserEmail"] = correoElectronico;
                    await Application.Current.SavePropertiesAsync(); // Guarda las propiedades para asegurar la persistencia.

                    Clear();
                    await Navigation.PushAsync(new HistorialTickets()); // Navegar a la página principal
                }
                else
                {
                    // Usuario no encontrado o contraseña incorrecta.
                    await DisplayAlert("Error", "Credenciales incorrectas", "Ok");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier otro error durante el proceso de inicio de sesión.
                await DisplayAlert("Error", "Error al iniciar sesión: " + ex.Message, "Ok");
                Clear();
            }
        }

        private async void btnCreate_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert(" ", "Se creará un usuario nuevo", "Ok");
            Clear();
            await Navigation.PushAsync(new Registro());

        }

        public void Clear()
        {
            txtCorreoElectronico.Text = string.Empty;
            txtContraseña.Text = string.Empty;
        }
    }
}