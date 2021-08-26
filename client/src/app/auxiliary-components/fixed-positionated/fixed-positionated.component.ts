import { Component, Input, OnInit, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-fixed-positionated',
  templateUrl: './fixed-positionated.component.html',
  styleUrls: ['./fixed-positionated.component.css']
})
export class FixedPositionatedComponent implements OnInit {
  @Input() templateRef: TemplateRef<any>;
  @Input() coordinateX: number;
  @Input() coordinateY: number; 

  constructor() { }

  ngOnInit(): void {
    
  }
}
