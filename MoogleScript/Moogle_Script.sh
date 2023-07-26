#!/bin/bash

#Funciones
    echo "  run                         Ejecutar el proyecto"
    echo "  report                      Compilar y generar el pdf del informe a partir del archivo en latex"
    echo "  show_report [tool]          Mostrar el informe con un visor del gusto del usuario, se ejecuta el comando y tiene 30S para igresar cual visor desea usar sino usa uno por defecto"
    echo "  slides                      Compilar y generar el pdf de la presentación a partir del archivo en latex"
    echo "  show_slides [tool]          Mostrar la presentacion con un visor del gusto del usuario, se ejecuta el comando y tiene 30S para igresar cual visor desea usar sino usa uno por defecto"
    echo "  clean                       Elimina ficheros auxiliares generados en la compilacion de la presentacion, el informe, y en la ejecucion del proyecto "
    echo ""

# Función para ejecutar el proyecto
# Obtener la ruta al directorio del script (directorio actual)
ruta_script="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# Ruta al directorio del programa (directorio padre del directorio del script)
ruta_programa="$(dirname "$ruta_script")"

# Ruta al directorio de la presentación (dentro del directorio padre)
ruta_presentacion="$ruta_programa/Presentacion"

# Ruta al directorio del informe (dentro del directorio padre)
ruta_informe="$ruta_programa/Informe"

# Función para ejecutar el programa
run() {
    # Cambiar al directorio del programa
    cd "$ruta_programa"

    # Comprobar el sistema operativo
    if [[ "$OSTYPE" == "linux-gnu"* ]]; then
        # Ejecutar el comando para Linux
        echo "Ejecutando el programa en Linux..."
        dotnet run --project "$ruta_programa/MoogleServer"
    elif [[ "$OSTYPE" == "msys" ]]; then
        # Ejecutar el comando para Windows
        echo "Ejecutando el programa en Windows..."
        dotnet watch run --project "$ruta_programa/MoogleServer"
    else
        echo "No se pudo determinar el sistema operativo."
    fi
}

    
# Función para compilar y generar el informe en PDF
report() {
# Ruta al archivo LaTeX del informe (en la carpeta "Informe")
    archivo_latex="$ruta_informe/Informe.tex"

    # Ruta de salida del archivo PDF del informe (en la carpeta "Informe")
    archivo_pdf="$ruta_informe/Informe.pdf"

    # Verificar si el archivo PDF ya existe
    if [[ -f "$archivo_pdf" ]]; then
        echo "El archivo PDF ya existe: $archivo_pdf"
    else
        # Verificar si el archivo LaTeX existe
        archivo_latex="$ruta_informe/Informe.tex"
        if [[ -f "$archivo_latex" ]]; then
            # Compilar el archivo LaTeX
            echo "Compilando archivo LaTeX: $archivo_latex"
            pdflatex -output-directory "$ruta_informe" "$archivo_latex"
        else
            echo "No se encontró ningún archivo LaTeX en la ruta especificada: $ruta_informe"
        fi
    fi
        # Restaurar el directorio de trabajo original
        cd "$directorio_actual"
}

show_report() {
    viewer=${1:-} # El visor de PDF personalizado es opcional

    if [[ "$OSTYPE" == "linux-gnu"* ]]; then
        # Estamos en Linux
        archivo_pdf="$ruta_informe/Informe.pdf"

        if [[ -f "$archivo_pdf" ]]; then
            # Si el archivo PDF existe, abre el informe en el visor personalizado o predeterminado
            if [[ -n "$viewer" ]] && command -v "$viewer" >/dev/null 2>&1; then
                echo "Abriendo el informe con el visor personalizado $viewer ..."
                $viewer "$archivo_pdf" &
            else
                echo "Abriendo el informe en el visor de PDFs predeterminado ..."
                xdg-open "$archivo_pdf" &
            fi
        else
            echo "No se encontró ningún archivo PDF en la ruta especificada: $ruta_informe"
        fi
    elif [[ "$OSTYPE" == "msys" ]]; then
        # Estamos en Windows (Git Bash)
        archivo_pdf="$ruta_informe/Informe.pdf"

        if [[ -f "$archivo_pdf" ]]; then
            # Si el archivo PDF existe, abre el informe en el visor personalizado o predeterminado
            if [[ -n "$viewer" ]] && command -v "$viewer" >/dev/null 2>&1; then
                echo "Abriendo el informe con el visor personalizado $viewer ..."
                "$viewer" "$archivo_pdf"
            else
                echo "Abriendo el informe en el visor de PDFs predeterminado ..."
                start "" "$archivo_pdf"
            fi
        else
            echo "No se encontró ningún archivo PDF en la ruta especificada: $ruta_informe"
        fi
    else
        # No se reconoce el sistema operativo
        echo "Esta función solo está disponible en Linux o Windows (Git Bash)."
        return 1
    fi
}

slides() {
    # Ruta al archivo LaTeX de la presentación (en la carpeta "Presentacion")
    archivo_latex="$ruta_presentacion/Presentacion.tex"

    # Ruta de salida del archivo PDF de la presentación (en la carpeta "Presentacion")
    archivo_pdf="$ruta_presentacion/Presentacion.pdf"

    # Verificar si el archivo PDF ya existe
    if [[ -f "$archivo_pdf" ]]; then
        echo "El archivo PDF ya existe: $archivo_pdf"
    else
        # Verificar si el archivo LaTeX existe
        archivo_latex="$ruta_presentacion/Presentacion.tex"
        if [[ -f "$archivo_latex" ]]; then
            # Compilar el archivo LaTeX
            echo "Compilando archivo LaTeX: $archivo_latex"
            pdflatex -output-directory "$ruta_presentacion" "$archivo_latex"
        else
            echo "No se encontró ningún archivo LaTeX en la ruta especificada: $ruta_presentacion"
        fi
    fi
        # Restaurar el directorio de trabajo original
        cd "$directorio_actual"
}

# Función para mostrar el informe en PDF
show_slides() {
    viewer=${1:-} # El visor de PDF personalizado es opcional

    if [[ "$OSTYPE" == "linux-gnu"* ]]; then
        # Estamos en Linux
        archivo_pdf="$ruta_presentacion/Presentacion.pdf"

        if [[ -f "$archivo_pdf" ]]; then
            # Si el archivo PDF existe, abre la presentación en el visor personalizado o predeterminado
            if [[ -n "$viewer" ]] && command -v "$viewer" >/dev/null 2>&1; then
                echo "Abriendo la presentación con el visor personalizado $viewer ..."
                $viewer "$archivo_pdf" &
            else
                echo "Abriendo la presentación en el visor de PDFs predeterminado ..."
                xdg-open "$archivo_pdf" &
            fi
        else
            echo "No se encontró ningún archivo PDF en la ruta especificada: $ruta_presentacion"
        fi
    elif [[ "$OSTYPE" == "msys" ]]; then
        # Estamos en Windows (Git Bash)
        archivo_pdf="$ruta_presentacion/Presentacion.pdf"

        if [[ -f "$archivo_pdf" ]]; then
            # Si el archivo PDF existe, abre la presentación en el visor personalizado o predeterminado
            if [[ -n "$viewer" ]] && command -v "$viewer" >/dev/null 2>&1; then
                echo "Abriendo la presentación con el visor personalizado $viewer ..."
                "$viewer" "$archivo_pdf"
            else
                echo "Abriendo la presentación en el visor de PDFs predeterminado ..."
                start "" "$archivo_pdf"
            fi
        else
            echo "No se encontró ningún archivo PDF en la ruta especificada: $ruta_presentacion"
        fi
    else
        # No se reconoce el sistema operativo
        echo "Esta función solo está disponible en Linux o Windows (Git Bash)."
        return 1
    fi
}


# Función para eliminar los archivos auxiliares
clean() {
    find $ruta_programa -type f \( -name "*.aux" -o -name "*.log" -o -name "*.toc" -o -name "*.nav" -o -name "*.out" -o -name "*.snm" \) -exec rm -f {} +
    echo "Eliminando los archivos auxiliares..."
}

# Analizar los argumentos de línea de comandos
case "$1" in
    run)
        run
        ;;
    report)
        report
        ;;
    slides)
        slides
        ;;
    show_report)
        show_report "$2"
        ;;
    show_slides)
        show_slides "$2"
        ;;
    clean)
        clean
        ;;
    *)
        echo "Opción no válida. Las opciones válidas son: run, report, slides, show_report, show_slides, clean"
        exit 1
        ;;
esac

exit 0