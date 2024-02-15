import { Component, Input, Renderer2, ElementRef } from '@angular/core';

@Component({
  selector: 'app-wizard-step',
  templateUrl: './wizard-step.component.html',
  styleUrl: './wizard-step.component.css'
})
export class WizardStepComponent {
  @Input() iconClass: string = "";
  @Input() index: number = 0;
  isActive: boolean = false;
currentStep: any;
stepsLength: any;

  constructor(public elementRef: ElementRef, private renderer: Renderer2) {}

  nextStep() {
    this.renderer.parentNode(this.elementRef.nativeElement).classList.remove('disabled');
    this.renderer.parentNode(this.elementRef.nativeElement).querySelector('a[data-toggle="tab"]').click();
  }

  previousStep() {
    this.renderer.parentNode(this.elementRef.nativeElement).querySelector('a[data-toggle="tab"]').click();
  }
}
