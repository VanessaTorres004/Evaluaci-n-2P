
namespace Evaluación_2P
{
    public partial class MainPage : ContentPage
    {
        private Recarga _recarga;

        public MainPage()
        {
            InitializeComponent();
            _recarga = new Recarga();
            BindingContext = _recarga;

            LoadLastRecarga();
        }

        private async void OnRecargaButtonClicked(object sender, EventArgs e)
        {
            // Validar entradas
            if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(PhoneEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar nombre y número telefónico.", "OK");
                return;
            }

            if (MontoPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Debe seleccionar un monto.", "OK");
                return;
            }

            if (OperadorPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Debe seleccionar un operador.", "OK");
                return;
            }

            string nombre = NameEntry.Text.Trim();
            string telefono = PhoneEntry.Text.Trim();
            string monto = MontoPicker.SelectedItem.ToString();
            string operador = OperadorPicker.SelectedItem.ToString();

            
            bool confirm = await DisplayAlert(
                "Confirmación",
                $"¿Confirma realizar una recarga de {monto} dólares a {telefono} ({operador})?",
                "Confirmar",
                "Cancelar");

            if (!confirm)
            {
                await DisplayAlert("Cancelación", "La recarga ha sido cancelada.", "OK");
                return;
            }

            
            string recargaInfo = $"Nombre: {nombre}\nTeléfono: {telefono}\nMonto: {monto} USD\nOperador: {operador}\nFecha: {DateTime.Now}\n";
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "UltimaRecarga.txt");
            File.WriteAllText(filePath, recargaInfo);

            
            _recarga.UltimaRecarga = recargaInfo;
            RecargaMessage.Text = "¡Recarga realizada con éxito!";
            await DisplayAlert("Éxito", "La recarga ha sido realizada exitosamente.", "OK");

            
            NameEntry.Text = string.Empty;
            PhoneEntry.Text = string.Empty;
            MontoPicker.SelectedIndex = -1;
            OperadorPicker.SelectedIndex = -1;
        }

        private void OnMontoSelected(object sender, EventArgs e)
        {
            if (MontoPicker.SelectedIndex != -1)
            {
                var monto = MontoPicker.SelectedItem.ToString();
                RecargaMessage.Text = $"Se ha seleccionado una recarga de {monto} dólares.";
            }
        }

        private void LoadLastRecarga()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "Vtorres.txt");

            if (File.Exists(filePath))
            {
                string lastRecarga = File.ReadAllText(filePath);
                _recarga.UltimaRecarga = lastRecarga;
                RecargaMessage.Text = "Última recarga cargada.";
            }
            else
            {
                _recarga.UltimaRecarga = "No hay recargas anteriores.";
                RecargaMessage.Text = "No hay recargas registradas.";
            }
        }
    }
}



