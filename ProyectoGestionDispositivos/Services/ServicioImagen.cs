using Firebase.Auth;
using Firebase.Storage;

namespace ProyectoGestionDispositivos.Services
{
    public class ServicioImagen : IServicioImagen
    {
        public async Task<string> SubirImagen(Stream archivo, string nombre)
        {
            string email = "juanpablohele@gmail.com";
            string clave = "Juanphl123.*";
            string storageBucket = "proyectogestiondispositivos.firebaseapp.com";
            string api_key = "AIzaSyC-Bo_uJjOA3xDmPjQ9JPXl7-SrWfwKs58";

            var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                storageBucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                
                .Child("Fotos_Perfil")
                .Child(nombre)
                .PutAsync(archivo, cancellation.Token);

            var downloadURL = await task;
            return "https://via.placeholder.com/200"; ;




        }
    }
}
