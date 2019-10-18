import { Pipe, PipeTransform } from '@angular/core';
import { City } from '../grid-batch-editing/city';

@Pipe({
  name: 'transformDates'
})
export class TransformDatesPipe implements PipeTransform {

  transform(cities: City[]): City [] {
    if (cities) {
      cities.forEach(city => city.HolidayDate = new Date(city.HolidayDate));
    }
    return cities;
  }

}
