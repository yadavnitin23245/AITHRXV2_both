import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
  name: 'filterData'
})
export class FilterPipe implements PipeTransform {
  transform(items: any[], searchText: string): any[] {
    debugger;
    if(!items) return [];
    if(!searchText) return items;
searchText = searchText.toLowerCase();

debugger;
// var dataitem=items.filter(it=>{

//   it.toString().includes(searchText);
// })

return items.filter( it => {
  debugger;
      return it.toLowerCase().includes(searchText);
    });
   }
}
