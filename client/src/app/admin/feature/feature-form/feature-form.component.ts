import { Component, Input, OnInit } from '@angular/core';
import { AdminProductFeature } from 'src/app/_models/adminModels/adminProductFeature';


@Component({
  selector: 'app-feature-form',
  templateUrl: './feature-form.component.html',
  styleUrls: ['./feature-form.component.css']
})
export class FeatureFormComponent implements OnInit {
  @Input() feature?: AdminProductFeature;

  constructor() { }

  ngOnInit(): void {
  }

}
