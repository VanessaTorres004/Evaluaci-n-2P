namespace Evaluación_2P;

public partial class HistorialPage : ContentPage
{
	public HistorialPage()
	{
		InitializeComponent();
	}
}
private void LoadHistorialRecargas()
    {
        var recargas = new List<Recarga>();

        // Aquí puedes cargar los datos desde archivos o desde una base de datos
        // Este es solo un ejemplo de cómo podrías hacerlo
        string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "*.txt");

        foreach (var file in files)
        {
            var contenido = File.ReadAllText(file);
            var datos = contenido.Split(',');

            // Asegúrate de que el archivo tenga al menos 2 elementos
            if (datos.Length >= 2)
            {
                recargas.Add(new Recarga
                {
                    PhoneNumber = datos[0],
                    MontoRecarga = datos[1]
                });
            }
        }

        RecargasListView.ItemsSource = recargas;
    }
}

public class Recarga
{
    public string PhoneNumber { get; set; }
    public string MontoRecarga { get; set; }
}
}