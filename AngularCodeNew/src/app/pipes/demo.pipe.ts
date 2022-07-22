import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'demo'
})
export class DemoPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): unknown {
    let v1=value.slice(0,10)
    return value;
  }

}
