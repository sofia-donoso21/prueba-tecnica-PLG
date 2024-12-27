import { TelefonoFormatPipe } from "./telefono-format.pipe";

describe('TelefonoFormatPipe', () => {
  let pipe: TelefonoFormatPipe;

  beforeEach(() => {
    // Crear una instancia del pipe antes de cada prueba
    pipe = new TelefonoFormatPipe();
  });

  it('debería formatear correctamente el número de teléfono', () => {
    // Caso de prueba: un número de teléfono válido
    const formattedPhone = pipe.transform('12345678');
    expect(formattedPhone).toBe('+569 1234 5678');
  });

  it('debería devolver el valor original si no tiene 8 dígitos', () => {
    // Caso de prueba: un número con menos de 8 dígitos
    const formattedPhone = pipe.transform('12345');
    expect(formattedPhone).toBe('+569 1234 5');
  });

  it('debería manejar números con caracteres no numéricos', () => {
    const formattedPhone = pipe.transform('12a3b4c5d6');
    console.log('Formatted Phone:', formattedPhone); // Verifica la salida
    expect(formattedPhone).toBe('+569 1234 56');
  });

  it('debería devolver vacío si no hay valor', () => {
    // Caso de prueba: sin valor
    const formattedPhone = pipe.transform('');
    expect(formattedPhone).toBe('');
  });
});
