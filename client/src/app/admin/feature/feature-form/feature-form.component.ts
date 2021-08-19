import { Component, Input, OnInit } from '@angular/core';
import { ProductFeature } from 'src/app/_models/adminProduct';

@Component({
  selector: 'app-feature-form',
  templateUrl: './feature-form.component.html',
  styleUrls: ['./feature-form.component.css']
})
export class FeatureFormComponent implements OnInit {
  @Input() feature?: ProductFeature;

  constructor() { }

  ngOnInit(): void {
  }

}
