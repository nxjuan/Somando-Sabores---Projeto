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
    'caurosel images/IMG - 1.png',
    'caurosel images/IMG - 2.png',
    'caurosel images/IMG - 3.png',
    'caurosel images/IMG - 4.png',
    'caurosel images/IMG - 5.png',
    'caurosel images/IMG - 6.png',
    'caurosel images/IMG - 7.png',
    'caurosel images/IMG - 8.png',
    'caurosel images/IMG - 9.png'    
  ]
}
