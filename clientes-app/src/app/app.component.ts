import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';  // Asegúrate de importar CommonModule
import { ClienteService } from './cliente.service';
import { HttpClientModule } from '@angular/common/http';
import { TelefonoFormatPipe } from './telefono-format.pipe';

@Component({
  selector: 'app-root',
  standalone: true,  // Este es un componente standalone
  imports: [
    CommonModule,
    TelefonoFormatPipe  ,
    HttpClientModule],  // Incluye CommonModule para que *ngFor y *ngIf funcionen
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [
    ClienteService
  ]
})
export class AppComponent {

  clientes: any[] = [];
  page: number = 1;
  pageSize: number = 10;
  title: any = 'Clientes App';

  constructor(private clienteService: ClienteService) { }

  ngOnInit(): void {
    this.cargarClientes();
  }

  // Método para cargar los clientes paginados
  cargarClientes(): void {
    this.clienteService.getClientesPaginado(this.page, this.pageSize).subscribe(
      (data) => {
        this.clientes = data;
        console.log("Data:::", data)
      },
      (error) => {
        console.error('Error al cargar los clientes:', error);
      }
    );
  }

  // Método para cambiar la página
  cambiarPagina(page: number): void {
    this.page = page;
    this.cargarClientes();
  }
}
