import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { toMessageType } from '../home/home.component';
import { eCapacity } from '../modules/conteiner/models/eCapacity';
import { eOperationType } from '../modules/conteiner/models/eOperationType';
import { ConteinerService } from '../modules/conteiner/services/conteiner.service';

@Component({
  selector: 'app-create-conteiner-component',
  templateUrl: './create.component.html'
})
export class CreateConteinerComponent {
  
  constructor(
    private formBuilder: FormBuilder,
    private service: ConteinerService
    ) {

  }

  readonly form: FormGroup = this.formBuilder.group({
    number: ['', Validators.compose([Validators.minLength(19), Validators.maxLength(19), Validators.required])],
    capacity: ['', Validators.required],
    operation: ['', [Validators.required]]
  });
  readonly number = this.form.get('number');
  readonly capacity = this.form.get('capacity');
  readonly operation = this.form.get('operation');

  
  sendForm() {
    if (this.form.invalid) {
      return;
    }
    
    this.service.create(this.form.value).subscribe();
  }
  
  operationsType() {
    return [toMessageType(eOperationType.Cabotagem),
            toMessageType(eOperationType.Exportacao),
            toMessageType(eOperationType.Importacao),
            toMessageType(eOperationType.Transbordo)];
  }

  capacities() {
    return [eCapacity.TWENTY, eCapacity.FOURTY];
  }
}

