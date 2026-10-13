using System;
using System.IO;

internal class Program
{
  static int max_emp = 50;
  static string[] nombres = new string[max_emp];
  static string[] correo_emp = new string[max_emp];
  static int cant_emp = 0;
  static string rutaArchivo = "empleado_datos.txt";

  static public void Titulo()
  {
    Console.WriteLine("****************************");
    Console.WriteLine("****SISTEMA DE EMPLEADOS****");
    Console.WriteLine("****************************");
  }

  static public void Registro_empleado()
  {
    Console.WriteLine("****************************");
    Console.WriteLine("*****REGISTRAR EMPLEADO*****");
    Console.WriteLine("****************************");

    if (cant_emp >= max_emp)
    {
      Console.WriteLine("Alcanzo la capacidad maxima de datos");
      return;
    }

    Console.WriteLine("Ingresar nombres y apellidos: ");
    string nombres_emp = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(nombres_emp))
    {
      Console.WriteLine("ERROR, el nombre no puede estar vacio");
      return;
    }

    string nombres_Mayus = nombres_emp.ToUpper();
    string[] partes = nombres_Mayus.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    string primer_nombre = partes[0];
    string primer_apellido = "";

    if (partes.Length > 1)
    {
       primer_apellido = partes[1];
    }

    string correo = primer_nombre.ToLower() + "." + primer_apellido.ToLower() + "@upn.edu.pe";
    
    nombres[cant_emp] = nombres_Mayus;
    correo_emp[cant_emp] = correo;
    cant_emp++;

    Console.WriteLine("Empleado registrado correctamente");
  }

  static public void buscar_empleado()
  {
    Console.WriteLine("****************************");
    Console.WriteLine("****BUSQUEDA DE EMPLEADO****");
    Console.WriteLine("****************************");

    if (cant_emp == 0)
    {
      Console.WriteLine("no hay registro de empleados");
      return;
    }

    Console.WriteLine("ingresar nombre a buscar: ");
    string buscar = Console.ReadLine()?.Trim().ToUpper();

    if (string.IsNullOrEmpty(buscar))
    {
      Console.WriteLine("ERROR, debe ingresar un texto a buscar");
      return;
    }

    bool encontrado = false;
    for (int i = 0; i < cant_emp; i++)
    {
      if (nombres[i].Contains(buscar))
      {
        Console.WriteLine("Encontrado: " + nombres[i] + " - correo: " + correo_emp[i]);
        encontrado = true;
      }
    }

    if (!encontrado)
    {
      Console.WriteLine("no se encuentran coincidencias");
    }
  }

  static public void ordenar()
  {
    if (cant_emp <= 1)
    {
      Console.WriteLine("No hay registros suficientes para ordenar");
      return;
    }

    for (int i = 0; i < cant_emp - 1; i++)
    {
      for (int j = 0; j < cant_emp - 1 - i; j++)
      {
        if (string.Compare(nombres[j], nombres[j + 1]) > 0)
        {
          string temp_nom = nombres[j];
          nombres[j] = nombres[j + 1];
          nombres[j + 1] = temp_nom;

          string temp_correo = correo_emp[j];
          correo_emp[j] = correo_emp[j + 1];
          correo_emp[j + 1] = temp_correo;
        }
      }
    }

    Console.WriteLine("Empleados ordenados alfabeticamente");
  }

  static public void mostrar()
  {
    Console.WriteLine("****************************");
    Console.WriteLine("*****LISTA DE EMPLEADOS*****");
    Console.WriteLine("****************************");

    if (cant_emp == 0)
    {
      Console.WriteLine("No hay registros por mostrar");
      return;
    }

    Console.WriteLine("Nombres y apellidos\t\tcorreo");
    for (int i = 0; i < cant_emp; i++)
    {
      Console.WriteLine(nombres[i] + "\t\t" + correo_emp[i]);
    }
  }

  static public void guardar_datos_archivo()
  {
    Console.WriteLine("****************************");
    Console.WriteLine("**GUARDAR DATOS EN ARCHIVO**");
    Console.WriteLine("****************************");

    if (cant_emp == 0)
    {
      Console.WriteLine("no hay registros por guardar");
      return;
    }

    using (StreamWriter escribir = new StreamWriter(rutaArchivo))
    {
      for (int i = 0; i < cant_emp; i++)
      {
        escribir.WriteLine(nombres[i] + "," + correo_emp[i]);
      }
    }

    Console.WriteLine(cant_emp + " Empleados guardados en el archivo");
  }

  static public void leer_datos_archivos()
  {
    Console.WriteLine("****************************");
    Console.WriteLine("****LEER DATOS DE ARCHIVO***");
    Console.WriteLine("****************************");

    if (!File.Exists(rutaArchivo))
    {
      Console.WriteLine("No existe el archivo de empleados");
      return;
    }

    int num_linea = 1;
    using (StreamReader lector = new StreamReader(rutaArchivo))
    {
      string linea;
      while ((linea = lector.ReadLine()) != null)
      {
        string[] partes = linea.Split(',');
        if (partes.Length == 2)
        {
          Console.WriteLine(num_linea + ".- nombres: " + partes[0] + " | correo: " + partes[1]);
          num_linea++;
        }
      }
    }
  }

  static public void cargar_datos_archivos()
  {
    
    Console.WriteLine("****************************");
    Console.WriteLine("***CARGAR DATOS DE ARCHIVO**");
    Console.WriteLine("****************************");

    if (!File.Exists(rutaArchivo))
    {
      Console.WriteLine("No existe el archivo de empleados");
      return;
    }

    cant_emp = 0;

    string[] lineas = File.ReadAllLines(rutaArchivo);
    for (int i = 0; i < lineas.Length && cant_emp < max_emp; i++)
    {
      string[] partes = lineas[i].Split(',');
      if (partes.Length == 2)
      {
        nombres[cant_emp] = partes[0];
        correo_emp[cant_emp] = partes[1];
        cant_emp++;
      }
    }

    Console.WriteLine(cant_emp + " Empleados cargados del archivo");
  }

  static void Main(string[] args)
  {
    int opc = 0;
    Titulo();

    while (opc != 8)
    {
      Console.WriteLine("[1] Registrar empleado");
      Console.WriteLine("[2] Buscar empleado");
      Console.WriteLine("[3] Ordenar alfabeticamente (A - Z)");
      Console.WriteLine("[4] Mostrar empleados");
      Console.WriteLine("[5] Guardar datos en archivo");
      Console.WriteLine("[6] Leer datos de archivo");
      Console.WriteLine("[7] Cargar datos de archivo");
      Console.WriteLine("[8] Salir");
      Console.WriteLine("Ingresar opcion: ");

      if (!int.TryParse(Console.ReadLine(), out opc))
      {
        Console.WriteLine("Ingresar un valor numerico");
        continue;
      }

      switch (opc)
      {
        case 1:
          Registro_empleado();
          break;
        case 2:
          buscar_empleado();
          break;
        case 3:
          ordenar();
          break;
        case 4:
          mostrar();
          break;
        case 5:
          guardar_datos_archivo();
          break;
        case 6:
          leer_datos_archivos();
          break;
        case 7:
          cargar_datos_archivos();
          break;
        case 8:
          Console.WriteLine("gracias por usar el sistema..!!");
          break;
        default:
          Console.WriteLine("Ingresar un numero del 1-8");
          break;
      }

      if (opc != 8)
      {
        Console.WriteLine("Presiona una tecla para continuar...");
        Console.ReadKey();
        Console.WriteLine();
      }
    }
  }
}