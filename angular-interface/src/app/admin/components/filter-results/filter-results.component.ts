import { Component } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';

@Component({
  selector: 'app-filter-results',
  imports: [MatIcon, MatExpansionModule],
  templateUrl: './filter-results.component.html',
  styleUrl: './filter-results.component.scss'
})
export class FilterResultsComponent {

}
