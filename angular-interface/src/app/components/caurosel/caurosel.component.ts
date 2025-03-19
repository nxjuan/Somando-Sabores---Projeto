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
  images = [
    'caurosel images/IMG - 1.PNG',
    'caurosel images/IMG - 2.PNG',
    'caurosel images/IMG - 3.PNG',
    'caurosel images/IMG - 4.PNG',
    'caurosel images/IMG - 5.PNG',
    'caurosel images/IMG - 6.PNG',
    'caurosel images/IMG - 7.PNG',
    'caurosel images/IMG - 8.PNG',
    'caurosel images/IMG - 9.PNG'    
  ]
}
