export interface Autor {
  autorId: number;
  nombre: string;
  apellido: string;
  fechaNacimiento: string | null;
  nacionalidad: string | null;
}

export interface AutorCreate {
  nombre: string;
  apellido: string;
  fechaNacimiento: string | null;
  nacionalidad: string | null;
}
