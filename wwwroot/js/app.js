
const API = "/Clientes";

let clientes = [];

// Obtener los clientes desde MySQL mediante la API
async function cargarClientes() {
    try {
        const respuesta = await fetch(
            `${API}/ListarUsuarios`
        );

        if (!respuesta.ok) {
            throw new Error("No se pudieron cargar los clientes");
        }

        clientes = await respuesta.json();
        mostrarClientes(clientes);

    } catch (error) {
        mostrarMensaje(error.message, "danger");
    }
}

// Mostrar registros en la tabla
function mostrarClientes(datos) {
    const tabla = document.getElementById("tablaClientes");
    tabla.innerHTML = "";

    datos.forEach(cliente => {
        const fila = document.createElement("tr");

        const valores = [
            cliente.idCliente,
            cliente.nombre,
            cliente.apellido,
            cliente.telefono
        ];

        valores.forEach(valor => {
            const celda = document.createElement("td");
            celda.textContent = valor ?? "";
            fila.appendChild(celda);
        });

        const acciones = document.createElement("td");

        const editar = document.createElement("button");
        editar.className = "btn btn-warning btn-sm me-2";
        editar.textContent = "Editar";
        editar.onclick = () => editarCliente(cliente.idCliente);

        const eliminar = document.createElement("button");
        eliminar.className = "btn btn-danger btn-sm";
        eliminar.textContent = "Eliminar";
        eliminar.onclick = () => eliminarCliente(cliente.idCliente);

        acciones.append(editar, eliminar);
        fila.appendChild(acciones);
        tabla.appendChild(fila);
    });
}

// Filtrar clientes por nombre o apellido
function filtrarClientes() {
    const texto = document.getElementById("busqueda")
        .value.toLowerCase();

    const filtrados = clientes.filter(c =>
        `${c.nombre ?? ""} ${c.apellido ?? ""}`
            .toLowerCase().includes(texto)
    );

    mostrarClientes(filtrados);
}

// Mostrar formulario para crear cliente
function nuevoCliente() {
    limpiarFormulario();
    document.getElementById("formulario").style.display = "block";
    document.getElementById("tituloFormulario").textContent =
        "Nuevo cliente";
}

// Mostrar datos del cliente seleccionado
function editarCliente(id) {
    const cliente = clientes.find(c => c.idCliente === id);

    if (!cliente) return;

    document.getElementById("idCliente").value = cliente.idCliente;
    document.getElementById("nombre").value = cliente.nombre ?? "";
    document.getElementById("apellido").value = cliente.apellido ?? "";
    document.getElementById("telefono").value = cliente.telefono ?? "";

    document.getElementById("tituloFormulario").textContent =
        "Editar cliente";

    document.getElementById("formulario").style.display = "block";
}

// Registrar o actualizar cliente
document.getElementById("formCliente")
    .addEventListener("submit", async function (event) {

        event.preventDefault();

        const id = document.getElementById("idCliente").value;

        const cliente = {
            idCliente: id ? Number(id) : 0,
            nombre: document.getElementById("nombre").value,
            apellido: document.getElementById("apellido").value,
            telefono: document.getElementById("telefono").value
        };

        const url = id
            ? `${API}/ActualizarUsuario/${id}`
            : `${API}/CrearUsuario`;

        const metodo = id ? "PUT" : "POST";

        try {
            const respuesta = await fetch(url, {
                method: metodo,
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(cliente)
            });

            if (!respuesta.ok) {
                throw new Error("No se pudo guardar el cliente");
            }

            mostrarMensaje(
                id ? "Cliente actualizado" : "Cliente registrado",
                "success"
            );

            limpiarFormulario();
            await cargarClientes();

        } catch (error) {
            mostrarMensaje(error.message, "danger");
        }
    });

// Eliminar cliente
async function eliminarCliente(id) {
    if (!confirm("¿Deseas eliminar este cliente?")) return;

    try {
        const respuesta = await fetch(
            `${API}/EliminarUsuario/${id}`,
            { method: "DELETE" }
        );

        if (!respuesta.ok) {
            throw new Error("No se pudo eliminar el cliente");
        }

        mostrarMensaje("Cliente eliminado", "success");
        await cargarClientes();

    } catch (error) {
        mostrarMensaje(error.message, "danger");
    }
}

// Limpiar y ocultar formulario
function limpiarFormulario() {
    document.getElementById("formCliente").reset();
    document.getElementById("idCliente").value = "";
    document.getElementById("formulario").style.display = "none";
}

// Mostrar mensajes al usuario
function mostrarMensaje(texto, tipo) {
    const mensaje = document.getElementById("mensaje");
    mensaje.textContent = texto;
    mensaje.className = `alert alert-${tipo}`;
}

// Cargar datos al abrir la página
cargarClientes();
