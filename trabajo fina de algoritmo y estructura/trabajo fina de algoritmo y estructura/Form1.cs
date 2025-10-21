namespace trabajo_fina_de_algoritmo_y_estructura
{
    // Agregamos System.Data para evitar conflicto con la clase Persona
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    public partial class Form1 : Form
    {
        public enum ModoFormulario { BUSCAR, AGREGAR, MODIFICAR, ELIMINAR }
        private ModoFormulario modoActual = ModoFormulario.BUSCAR;

        private PersonaAccessDAO personaDAO = new PersonaAccessDAO();
        private Persona personaSeleccionada = null;


        public Form1()
        {
            InitializeComponent();

            // Configurar eventos de filtrado autom�tico
            txtDniBusqueda.TextChanged += txtBusqueda_TextChanged;
            txtApellidoBusqueda.TextChanged += txtBusqueda_TextChanged;

            // Iniciar la vista en modo BUSCAR
            ActualizarVista(ModoFormulario.BUSCAR);
        }

        private void ActualizarVista(ModoFormulario modo)
        {
            modoActual = modo;

            // Si volvemos de un modo de edici�n, limpiamos la persona seleccionada
            if (modo == ModoFormulario.BUSCAR)
            {
                personaSeleccionada = null;
            }

            // ===============================================================
            // 1. Visibilidad y Habilitaci�n de campos de DATOS (Nombre y Tel�fono)
            // ===============================================================

            // CORRECCI�N/OPTIMIZACI�N: Mostrar campos si estamos editando, O si estamos en BUSCAR
            // pero hay una persona seleccionada.
            bool mostrarCamposDatos = (modo != ModoFormulario.BUSCAR) || (personaSeleccionada != null);

            // La edici�n est� habilitada SOLO en modo AGREGAR o MODIFICAR
            bool habilitarEdicion = (modo == ModoFormulario.AGREGAR || modo == ModoFormulario.MODIFICAR);

            // Visibilidad
            lblNombre.Visible = mostrarCamposDatos;
            txtNombre.Visible = mostrarCamposDatos;
            lblTelefono.Visible = mostrarCamposDatos;
            txtTelefono.Visible = mostrarCamposDatos;

            // Habilitaci�n
            txtNombre.Enabled = habilitarEdicion;
            txtTelefono.Enabled = habilitarEdicion;

            // ===============================================================
            // 2. Control de ListBox, DNI, y Apellido
            // ===============================================================

            // El ListBox solo es visible en modo BUSCAR.
            lstResultados.Visible = (modo == ModoFormulario.BUSCAR);
            lstResultados.Enabled = (modo == ModoFormulario.BUSCAR);

            // DNI y Apellido siempre visibles
            txtDniBusqueda.Visible = true;
            txtApellidoBusqueda.Visible = true;

            // Habilitaci�n de DNI y Apellido: Solo para BUSCAR (filtrar) o AGREGAR (clave nueva)
            txtDniBusqueda.Enabled = (modo == ModoFormulario.BUSCAR || modo == ModoFormulario.AGREGAR);
            txtApellidoBusqueda.Enabled = (modo == ModoFormulario.BUSCAR || modo == ModoFormulario.AGREGAR);

            // ===============================================================
            // 3. Visibilidad de BOTONES (ACCIONES Y GUARDAR/CANCELAR)
            // ===============================================================
            bool mostrarBotonesInicio = (modo == ModoFormulario.BUSCAR);

            // Botones de inicio (Agregar, Modificar, Eliminar)
            btnAgregar.Visible = mostrarBotonesInicio;
            btnModificar.Visible = mostrarBotonesInicio;
            btnEliminar.Visible = mostrarBotonesInicio;

            // Botones de acci�n (Guardar, Cancelar)
            btnGuardar.Visible = !mostrarBotonesInicio;
            btnCancelar.Visible = !mostrarBotonesInicio;

            // Configura el texto del bot�n Guardar 
            btnGuardar.Text = (modo == ModoFormulario.ELIMINAR) ? "Confirmar Eliminaci�n" : "Guardar";

            // ===============================================================
            // 4. L�gica de Limpieza y Carga (Consolidada)
            // ===============================================================
            if (modo == ModoFormulario.AGREGAR)
            {
                // Limpiar todos los campos para la nueva entrada
                LimpiarCamposBusqueda();
                LimpiarCamposDatos();
            }
            else if (modo == ModoFormulario.BUSCAR)
            {
                LimpiarCamposDatos();
                // Forzar la recarga de la lista con los filtros actuales
                txtBusqueda_TextChanged(null, null);
            }
        }



        private void LimpiarCamposBusqueda()
        {
            txtDniBusqueda.Text = "";
            txtApellidoBusqueda.Text = "";
        }

        private void LimpiarCamposDatos()
        {
            txtNombre.Text = "";
            txtTelefono.Text = "";
        }

        // ===============================================
        // M�TODOS DE B�SQUEDA Y FILTRADO AUTOM�TICO
        // ===============================================

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (modoActual != ModoFormulario.BUSCAR) return;

            string dniFiltro = txtDniBusqueda.Text.Trim();
            string apellidoFiltro = txtApellidoBusqueda.Text.Trim();

            // OPTIMIZACI�N: Limpiar la persona seleccionada antes de recargar la lista
            // Esto previene que el bot�n Modificar/Eliminar use una persona "vieja"
            personaSeleccionada = null;
            LimpiarCamposDatos();

            // CAMBIO CLAVE: Llamada al DAO para obtener los datos de SQL
            List<Persona> resultados = personaDAO.ObtenerPersonas(dniFiltro, apellidoFiltro);

            // 2. Establecer la fuente de datos del ListBox
            lstResultados.DataSource = null;
            lstResultados.DataSource = resultados;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Limpiar todos los campos antes de entrar en modo Agregar
            LimpiarCamposBusqueda(); // Limpia DNI y Apellido
            LimpiarCamposDatos();    // Limpia Nombre y Tel�fono

            ActualizarVista(ModoFormulario.AGREGAR);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // 1. Validaci�n de selecci�n
            if (personaSeleccionada == null)
            {
                MessageBox.Show("Seleccione una persona de la lista primero.");
                return;
            }

            // 2. Cargar los datos a los TextBox antes de cambiar la vista.
            // Aunque ya se cargaron al hacer clic en la lista, esto asegura que est�n actualizados.
            txtDniBusqueda.Text = personaSeleccionada.Dni;
            txtApellidoBusqueda.Text = personaSeleccionada.Apellido;
            txtNombre.Text = personaSeleccionada.Nombre;
            txtTelefono.Text = personaSeleccionada.Telefono;

            // 3. Cambiar el modo.
            // Esto oculta la lista y los botones de inicio, y muestra Guardar/Cancelar.
            // Tambi�n habilita la edici�n de txtNombre y txtTelefono (Ver secci�n 1).
            ActualizarVista(ModoFormulario.MODIFICAR);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (personaSeleccionada == null)
            {
                MessageBox.Show("Seleccione una persona de la lista primero.");
                return;
            }

            // 1. Cargar los datos a los TextBox (aunque se hizo al seleccionar, lo forzamos)
            txtDniBusqueda.Text = personaSeleccionada.Dni;
            txtApellidoBusqueda.Text = personaSeleccionada.Apellido;
            txtNombre.Text = personaSeleccionada.Nombre;
            txtTelefono.Text = personaSeleccionada.Telefono;

            // 2. Cambiar al modo Eliminar.
            // Esto hace visibles Guardar/Cancelar y deshabilita la edici�n de todos los campos.
            ActualizarVista(ModoFormulario.ELIMINAR);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Vuelve al modo BUSCAR, lo cual limpia la vista de edici�n y regresa los botones principales.
            ActualizarVista(ModoFormulario.BUSCAR);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool exito = false;

            // Recolecci�n de datos comunes
            Persona personaAOperar = new Persona
            {
                Dni = txtDniBusqueda.Text.Trim(),
                Apellido = txtApellidoBusqueda.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Telefono = txtTelefono.Text.Trim()
            };

            // 1. L�gica de Eliminaci�n (SQL)
            if (modoActual == ModoFormulario.ELIMINAR)
            {
                // La personaSeleccionada NO ES NULA aqu� porque se valid� en btnEliminar_Click
                exito = personaDAO.EliminarPersona(personaSeleccionada.Dni);
                if (exito)
                {
                    MessageBox.Show($"Persona con DNI {personaSeleccionada.Dni} ELIMINADA.");
                }
            }
            // 2. L�gica de Modificaci�n (SQL)
            else if (modoActual == ModoFormulario.MODIFICAR)
            {
                // Usamos la persona recolectada (personaAOperar) para el Update
                exito = personaDAO.ModificarPersona(personaAOperar);
                if (exito)
                {
                    MessageBox.Show($"Persona con DNI {personaAOperar.Dni} MODIFICADA.");
                }
            }
            // 3. L�gica de Agregar (SQL)
            else if (modoActual == ModoFormulario.AGREGAR)
            {
                if (string.IsNullOrWhiteSpace(personaAOperar.Dni) || string.IsNullOrWhiteSpace(personaAOperar.Apellido))
                {
                    MessageBox.Show("DNI y Apellido son obligatorios para agregar.");
                    return;
                }

                exito = personaDAO.AgregarPersona(personaAOperar);
                if (exito)
                {
                    MessageBox.Show($"Nueva persona agregada con DNI {personaAOperar.Dni}.");
                }
            }

            // Siempre regresa al modo de b�squeda despu�s de una acci�n exitosa
            if (exito)
            {
                ActualizarVista(ModoFormulario.BUSCAR);
            }
        }
        
        /* ... (Resto de m�todos como lstResultados_SelectedIndexChanged_1, label2_Click) ... */
        

        private void lstResultados_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // 1. Obtener el objeto seleccionado de forma segura
            Persona tempPersona = lstResultados.SelectedItem as Persona;

            if (tempPersona == null)
            {
                personaSeleccionada = null;
                LimpiarCamposDatos();
                // OPTIMIZACI�N: Limpiar tambi�n los campos de b�squeda/clave si la lista se vac�a
                LimpiarCamposBusqueda();
                return;
            }

            // 2. Si llegamos aqu�, tenemos una persona v�lida
            personaSeleccionada = tempPersona;

            // Cargar los datos de la persona seleccionada en los TextBox
            txtDniBusqueda.Text = personaSeleccionada.Dni;
            txtApellidoBusqueda.Text = personaSeleccionada.Apellido;
            txtNombre.Text = personaSeleccionada.Nombre;
            txtTelefono.Text = personaSeleccionada.Telefono;

            // *** ELIMINAR ESTA L�NEA O COMENTARLA ***
            // ActualizarVista(ModoFormulario.BUSCAR); 

            // SUGERENCIA: Aqu� podr�amos habilitar la visibilidad de Nombre/Tel�fono,
            // pero lo mejor es usar la optimizaci�n del punto 2.
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
