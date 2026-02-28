import { Component, OnInit } from '@angular/core';
import { LibroService } from '../../services/libro.service';
import { AutorService } from '../../services/autor.service';
import { ToastService } from '../../services/toast.service';
import { Libro, LibroCreate } from '../../models/libro.model';
import { Autor } from '../../models/autor.model';

declare var bootstrap: any;

@Component({
  selector: 'app-libros',
  standalone: false,
  templateUrl: './libros.component.html'
})
export class LibrosComponent implements OnInit {
  libros: Libro[] = [];
  filteredLibros: Libro[] = [];
  autores: Autor[] = [];
  searchTerm: string = '';
  isEditing: boolean = false;
  selectedLibro: Libro | null = null;

  formData: LibroCreate = {
    titulo: '',
    genero: null,
    fechaPublicacion: null,
    isbn: '',
    autorIds: []
  };

  deleteTarget: Libro | null = null;
  private modal: any;
  private deleteModal: any;

  constructor(
    private libroService: LibroService,
    private autorService: AutorService,
    private toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.loadLibros();
    this.loadAutores();
  }

  loadLibros(): void {
    this.libroService.getLibros().subscribe({
      next: (data) => {
        this.libros = data;
        this.filterLibros();
      },
      error: () => {
        this.toastService.error('Error al cargar los libros');
      }
    });
  }

  loadAutores(): void {
    this.autorService.getAutores().subscribe({
      next: (data) => {
        this.autores = data;
      },
      error: () => {
        this.toastService.error('Error al cargar los autores');
      }
    });
  }

  filterLibros(): void {
    const term = this.searchTerm.toLowerCase().trim();
    if (!term) {
      this.filteredLibros = [...this.libros];
    } else {
      this.filteredLibros = this.libros.filter(l =>
        l.titulo.toLowerCase().includes(term) ||
        (l.genero && l.genero.toLowerCase().includes(term)) ||
        l.isbn.toLowerCase().includes(term) ||
        l.autores.some(a =>
          (a.nombre + ' ' + a.apellido).toLowerCase().includes(term)
        )
      );
    }
  }

  openAddModal(): void {
    this.isEditing = false;
    this.selectedLibro = null;
    this.formData = { titulo: '', genero: null, fechaPublicacion: null, isbn: '', autorIds: [] };
    this.modal = new bootstrap.Modal(document.getElementById('libroModal'));
    this.modal.show();
  }

  openEditModal(libro: Libro): void {
    this.isEditing = true;
    this.selectedLibro = libro;
    this.formData = {
      titulo: libro.titulo,
      genero: libro.genero,
      fechaPublicacion: libro.fechaPublicacion ? libro.fechaPublicacion.split('T')[0] : null,
      isbn: libro.isbn,
      autorIds: libro.autores.map(a => a.autorId)
    };
    this.modal = new bootstrap.Modal(document.getElementById('libroModal'));
    this.modal.show();
  }

  openDeleteModal(libro: Libro): void {
    this.deleteTarget = libro;
    this.deleteModal = new bootstrap.Modal(document.getElementById('deleteLibroModal'));
    this.deleteModal.show();
  }

  toggleAutor(autorId: number): void {
    const index = this.formData.autorIds.indexOf(autorId);
    if (index > -1) {
      this.formData.autorIds.splice(index, 1);
    } else {
      this.formData.autorIds.push(autorId);
    }
  }

  isAutorSelected(autorId: number): boolean {
    return this.formData.autorIds.includes(autorId);
  }

  getAutoresText(libro: Libro): string {
    if (!libro.autores || libro.autores.length === 0) return '—';
    return libro.autores.map(a => a.nombre + ' ' + a.apellido).join(', ');
  }

  saveLibro(): void {
    if (this.isEditing && this.selectedLibro) {
      this.libroService.updateLibro(this.selectedLibro.libroId, this.formData).subscribe({
        next: () => {
          this.toastService.success('Se guardó correctamente');
          this.modal.hide();
          this.loadLibros();
        },
        error: () => {
          this.toastService.error('Error al actualizar el libro');
        }
      });
    } else {
      this.libroService.createLibro(this.formData).subscribe({
        next: () => {
          this.toastService.success('Se guardó correctamente');
          this.modal.hide();
          this.loadLibros();
        },
        error: () => {
          this.toastService.error('Error al crear el libro');
        }
      });
    }
  }

  confirmDelete(): void {
    if (this.deleteTarget) {
      this.libroService.deleteLibro(this.deleteTarget.libroId).subscribe({
        next: () => {
          this.toastService.success('Se eliminó correctamente');
          this.deleteModal.hide();
          this.loadLibros();
        },
        error: () => {
          this.toastService.error('Error al eliminar el libro');
        }
      });
    }
  }

  formatDate(date: string | null): string {
    if (!date) return '—';
    return new Date(date).toLocaleDateString('es-ES', {
      year: 'numeric',
      month: 'short',
      day: 'numeric'
    });
  }
}
