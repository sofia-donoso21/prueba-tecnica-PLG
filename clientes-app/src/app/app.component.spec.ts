import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component'; // Asegúrate de que esta importación sea correcta

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppComponent] // Cambiar 'declarations' por 'imports'
    }).compileComponents();
  });

  it('debería representar el título en una etiqueta h1', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('h1').textContent).toContain('Clientes App');  // Cambia aquí el texto esperado
  });

});
