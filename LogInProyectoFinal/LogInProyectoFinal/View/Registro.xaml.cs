using LogInProyectoFinal.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LogInProyectoFinal.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        UsuarioRepository loginRepository = new UsuarioRepository();
        public Registro()
        {
            InitializeComponent();
        }

        private async void btnCrearUsuario_Clicked(object sender, EventArgs e)
        {
            string nombre = nametxt.Text;
            string apellidos = txtApellidos.Text;
            string telefono = txtTelefono.Text;
            string departamento = txtDepartamento.Text;
            string correoElectronico = txtCorreo.Text;
            string contraseña = txtContraseña.Text;

            if (string.IsNullOrEmpty(nombre))
            {
                await DisplayAlert("Alerta", "Ingrese su nombre", "Ok");
            }
            if (string.IsNullOrEmpty(apellidos))
            {
                await DisplayAlert("Alerta", "Ingrese su apellidos", "Ok");
            }
            if (string.IsNullOrEmpty(telefono))
            {
                await DisplayAlert("Alerta", "Ingrese su telefono", "Ok");
            }
            if (string.IsNullOrEmpty(departamento))
            {
                await DisplayAlert("Alerta", "Ingrese su direccion", "Ok");
            }
            if (string.IsNullOrEmpty(correoElectronico))
            {
                await DisplayAlert("Alerta", "Ingrese su correo electronico", "Ok");
            }
            if (string.IsNullOrEmpty(contraseña))
            {
                await DisplayAlert("Alerta", "Ingrese su contraseña", "Ok");
            }
            Usuario login = new Usuario();
            login.Nombre = nombre;
            login.Apellidos = apellidos;
            login.Telefono = telefono;
            login.Departamento = departamento;
            login.CorreoElectronico = correoElectronico;
            login.Contraseña = contraseña;

            var isSaved = await loginRepository.Save(login);
            if (isSaved)
            {
                await DisplayAlert(" ", "Usuario Creado", "Ok");
                Clear();
                ((NavigationPage)this.Parent).PushAsync(new LogIn());
            }
            else
            {
                await DisplayAlert(" ", "Fallo el guardar", "Ok");
            }
        }
        public void Clear()
        {
            nametxt.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDepartamento.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtContraseña.Text = string.Empty;
        }
    }
}