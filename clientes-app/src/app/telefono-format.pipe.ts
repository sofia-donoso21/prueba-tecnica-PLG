import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'telefonoFormat'
})
export class TelefonoFormatPipe implements PipeTransform {

  transform(value: string): string {
    if (!value) return value; // Si el valor está vacío, retorna el mismo valor

    // Eliminar cualquier carácter no numérico
    const phone = value.replace(/\D/g, '');

    // Si el número tiene más de 8 dígitos, lo recortamos a 8
    if (phone.length >= 8) {
      return `+569 ${phone.slice(0, 4)} ${phone.slice(4, 8)}`;
    } else {
      // Si tiene menos de 8 dígitos, formateamos según el número de dígitos
      return `+569 ${phone.slice(0, 4)} ${phone.slice(4)}`;
    }
  }
}
