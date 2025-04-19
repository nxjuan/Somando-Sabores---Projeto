import { Component, OnInit } from '@angular/core';
import { register } from 'swiper/element/bundle';
import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-caurosel',
  imports: [ CommonModule ],
  templateUrl: './caurosel.component.html',
  styleUrl: './caurosel.component.scss',
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class CauroselComponent implements OnInit{
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

  ngOnInit(): void {
    register();
  }

  onSwiperInit(swiper: any) {
    setTimeout(() => swiper.update(), 200);
  }
}
