export interface LibroAutor {
  autorId: number;
  nombre: string;
  apellido: string;
  fechaNacimiento: string | null;
  nacionalidad: string | null;
}

export interface Libro {
  libroId: number;
  titulo: string;
  genero: string | null;
  fechaPublicacion: string | null;
  isbn: string;
  autores: LibroAutor[];
}

export interface LibroCreate {
  titulo: string;
  genero: string | null;
  fechaPublicacion: string | null;
  isbn: string;
  autorIds: number[];
}
