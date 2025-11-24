using BibliotecaDigital.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibliotecaDigital
{
    public partial class Form1 : Form
    {
        List<Libro> biblioteca = new List<Libro>();
        public Form1()
        {
            InitializeComponent();
            CargarDatos();
            cmbOpciones.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CargarDatos()
        {
            biblioteca.Add(new Libro { Codigo = "001", Titulo = "Cien Años de Soledad", Autor = "Gabriel Garcia Marquez", Año = 1967, Descripcion = "Historia de la familia Buendia en Macondo." });
            biblioteca.Add(new Libro { Codigo = "005", Titulo = "Don Quijote", Autor = "Miguel de Cervantes", Año = 1605, Descripcion = "Un hidalgo pierde la cordura por leer libros de caballeria." });
            biblioteca.Add(new Libro { Codigo = "003", Titulo = "1984", Autor = "George Orwell", Año = 1949, Descripcion = "Una distopia sobre el control totalitario y el Gran Hermano." });
            biblioteca.Add(new Libro { Codigo = "002", Titulo = "El Principito", Autor = "Antoine de Saint-Exupery", Año = 1943, Descripcion = "Un piloto se encuentra con un pequeño principe en el desierto." });
            biblioteca.Add(new Libro { Codigo = "004", Titulo = "Ficciones", Autor = "Jorge Luis Borges", Año = 1944, Descripcion = "Cuentos complejos sobre laberintos y espejos." });
            biblioteca.Add(new Libro { Codigo = "006", Titulo = "Rayuela", Autor = "Julio Cortazar", Año = 1963, Descripcion = "Novela que se puede leer en varios ordenes." });
            biblioteca.Add(new Libro { Codigo = "007", Titulo = "Pedro Paramo", Autor = "Juan Rulfo", Año = 1955, Descripcion = "Un hombre busca a su padre en un pueblo fantasma." });

            ActualizarGrid(biblioteca);
        }

        private void ActualizarGrid(List<Libro> listaMostrar)
        {
            dgvLibros.DataSource = null; 
            dgvLibros.DataSource = listaMostrar; 
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = tbBuscar.Text.Trim();
            if (string.IsNullOrEmpty(busqueda))
            {
                MessageBox.Show("Escribe algo para buscar.");
                return;
            }

            List<Libro> resultados = new List<Libro>();
            string criterio = cmbOpciones.SelectedItem.ToString();

            
            switch (criterio)
            {
                case "Título (Lineal)":
                    
                    foreach (var libro in biblioteca)
                    {
                        if (libro.Titulo.IndexOf(busqueda, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            resultados.Add(libro);
                        }
                    }
                    break;

                case "Autor (Binaria)":
                   
                    
                    biblioteca.Sort((x, y) => string.Compare(x.Autor, y.Autor));
                    ActualizarGrid(biblioteca); 

                    int izquierda = 0;
                    int derecha = biblioteca.Count - 1;
                    bool encontrado = false;

                    while (izquierda <= derecha)
                    {
                        int medio = (izquierda + derecha) / 2;
                        Libro libroMedio = biblioteca[medio];

                        int comparacion = string.Compare(libroMedio.Autor, busqueda, StringComparison.OrdinalIgnoreCase);

                        if (comparacion == 0)
                        {
                            resultados.Add(libroMedio);
                            encontrado = true;
                            break; 
                        }
                        if (comparacion < 0) izquierda = medio + 1;
                        else derecha = medio - 1;
                    }

                    if (!encontrado) MessageBox.Show("En búsqueda binaria exacta, el nombre debe ser preciso.");
                    break;

                case "Descripción (Texto)":
                    
                    foreach (var libro in biblioteca)
                    {
                        if (libro.Descripcion.IndexOf(busqueda, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            resultados.Add(libro);
                        }
                    }
                    break;
            }

            
            if (resultados.Count > 0)
            {
                ActualizarGrid(resultados);
            }
            else
            {
                MessageBox.Show("No se encontraron coincidencias.");
                ActualizarGrid(biblioteca); 
            }
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            tbBuscar.Clear();
            ActualizarGrid(biblioteca);
        }

        private void btnAnalisis_Click(object sender, EventArgs e)
        {
            if (biblioteca.Count == 0) return;

            Libro masAntiguo = biblioteca[0];
            Libro masReciente = biblioteca[0];

            // ALGORITMO 4: RECORRIDO PARA MAX Y MIN
            foreach (var libro in biblioteca)
            {
                if (libro.Año < masAntiguo.Año) masAntiguo = libro;
                if (libro.Año > masReciente.Año) masReciente = libro;
            }

            string mensaje = $"Libro más ANTIGUO:\n{masAntiguo.Titulo} ({masAntiguo.Año})\n\n" +
                             $"Libro más RECIENTE:\n{masReciente.Titulo} ({masReciente.Año})";

            MessageBox.Show(mensaje, "Análisis de Colección", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
    

