using Firebase.Database;
using Firebase.Database.Query;
using LogInProyectoFinal.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInProyectoFinal.Service
{
    internal class LoginRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://contact0-1aa15-default-rtdb.firebaseio.com/");
        public async Task<bool> Save(Usuario client)
        {
            var data = await firebaseClient.Child(nameof(Usuario)).PostAsync(JsonConvert.SerializeObject(client));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }
        public async Task<List<Usuario>> GetAll()
        {
            return (await firebaseClient.Child(nameof(Usuario)).OnceAsync<Usuario>()).Select(item => new Usuario
            {
                CorreoElectronico = item.Object.CorreoElectronico,
                Contraseña = item.Object.Contraseña,

            }).ToList();
        }
        public async Task<Usuario> GetUserByEmail(string correoElectronico)
        {
            // Realizar una consulta para buscar un usuario por correo electrónico.
            var usuarios = await firebaseClient
                .Child("Usuario")
                .OrderBy("CorreoElectronico")
                .EqualTo(correoElectronico)
                .OnceAsync<Usuario>();

            // Si se encuentra al menos un usuario, devuelve el primero. 
            // En teoría, el correo electrónico debería ser único, por lo que solo debería haber un resultado.
            return usuarios.FirstOrDefault()?.Object;
        }

        public async Task<List<Usuario>> GetAllByName(string CorreoElectronico)
        {
            return (await firebaseClient.Child(nameof(Usuario)).OnceAsync<Usuario>()).Select(item => new Usuario
            {
                CorreoElectronico = item.Object.CorreoElectronico,
                Contraseña = item.Object.Contraseña,
            }).Where(c => c.CorreoElectronico.ToLower().Contains(CorreoElectronico.ToLower())).ToList();
        }

        public async Task<bool> Update(Usuario CorreoElectronico)
        {
            await
            firebaseClient.Child(nameof(Usuario) + "/" + CorreoElectronico.CorreoElectronico).PutAsync(JsonConvert.SerializeObject(CorreoElectronico));
            return true;
        }
        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(Usuario) + "/" + id).DeleteAsync();
            return true;
        }
        public async Task<Usuario> GetById(string id)
        {
            return await firebaseClient.Child(nameof(Usuario) + "/" + id).OnceSingleAsync<Usuario>();
        }
    }
}
