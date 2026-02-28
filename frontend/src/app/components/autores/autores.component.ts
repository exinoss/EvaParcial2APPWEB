import { Component, OnInit } from '@angular/core';
import { AutorService } from '../../services/autor.service';
import { ToastService } from '../../services/toast.service';
import { Autor, AutorCreate } from '../../models/autor.model';

declare var bootstrap: any;

@Component({
  selector: 'app-autores',
  standalone: false,
  templateUrl: './autores.component.html'
})
export class AutoresComponent implements OnInit {
  autores: Autor[] = [];
  filteredAutores: Autor[] = [];
  searchTerm: string = '';
  isEditing: boolean = false;
  selectedAutor: Autor | null = null;

  formData: AutorCreate = {
    nombre: '',
    apellido: '',
    fechaNacimiento: null,
    nacionalidad: null
  };

  deleteTarget: Autor | null = null;
  private modal: any;
  private deleteModal: any;

  constructor(
    private autorService: AutorService,
    private toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.loadAutores();
  }

  loadAutores(): void {
    this.autorService.getAutores().subscribe({
      next: (data) => {
        this.autores = data;
        this.filterAutores();
      },
      error: () => {
        this.toastService.error('Error al cargar los autores');
      }
    });
  }

  filterAutores(): void {
    const term = this.searchTerm.toLowerCase().trim();
    if (!term) {
      this.filteredAutores = [...this.autores];
    } else {
      this.filteredAutores = this.autores.filter(a =>
        a.nombre.toLowerCase().includes(term) ||
        a.apellido.toLowerCase().includes(term) ||
        (a.nacionalidad && a.nacionalidad.toLowerCase().includes(term))
      );
    }
  }

  openAddModal(): void {
    this.isEditing = false;
    this.selectedAutor = null;
    this.formData = { nombre: '', apellido: '', fechaNacimiento: null, nacionalidad: null };
    this.modal = new bootstrap.Modal(document.getElementById('autorModal'));
    this.modal.show();
  }

  openEditModal(autor: Autor): void {
    this.isEditing = true;
    this.selectedAutor = autor;
    this.formData = {
      nombre: autor.nombre,
      apellido: autor.apellido,
      fechaNacimiento: autor.fechaNacimiento ? autor.fechaNacimiento.split('T')[0] : null,
      nacionalidad: autor.nacionalidad
    };
    this.modal = new bootstrap.Modal(document.getElementById('autorModal'));
    this.modal.show();
  }

  openDeleteModal(autor: Autor): void {
    this.deleteTarget = autor;
    this.deleteModal = new bootstrap.Modal(document.getElementById('deleteAutorModal'));
    this.deleteModal.show();
  }

  saveAutor(): void {
    if (this.isEditing && this.selectedAutor) {
      this.autorService.updateAutor(this.selectedAutor.autorId, this.formData).subscribe({
        next: () => {
          this.toastService.success('Se guardó correctamente');
          this.modal.hide();
          this.loadAutores();
        },
        error: () => {
          this.toastService.error('Error al actualizar el autor');
        }
      });
    } else {
      this.autorService.createAutor(this.formData).subscribe({
        next: () => {
          this.toastService.success('Se guardó correctamente');
          this.modal.hide();
          this.loadAutores();
        },
        error: () => {
          this.toastService.error('Error al crear el autor');
        }
      });
    }
  }

  confirmDelete(): void {
    if (this.deleteTarget) {
      this.autorService.deleteAutor(this.deleteTarget.autorId).subscribe({
        next: () => {
          this.toastService.success('Se eliminó correctamente');
          this.deleteModal.hide();
          this.loadAutores();
        },
        error: () => {
          this.toastService.error('Error al eliminar el autor');
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
