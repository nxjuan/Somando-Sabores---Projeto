import { Component } from '@angular/core';
import { CarouselModule } from 'primeng/carousel';

@Component({
  standalone: true,
  selector: 'app-caurosel',
  imports: [CarouselModule],
  templateUrl: './caurosel.component.html',
  styleUrl: './caurosel.component.scss'
})
export class CauroselComponent {

}
