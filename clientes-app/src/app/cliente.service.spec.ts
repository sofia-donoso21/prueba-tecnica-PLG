import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';  // Importar el módulo de pruebas para HttpClient
import { ClienteService } from './cliente.service';

describe('ClienteService', () => {
  let service: ClienteService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],  // Importar el módulo de pruebas
      providers: [ClienteService]
    });
    service = TestBed.inject(ClienteService);
  });

  it('debería ser creado', () => {
    expect(service).toBeTruthy();
  });
});
