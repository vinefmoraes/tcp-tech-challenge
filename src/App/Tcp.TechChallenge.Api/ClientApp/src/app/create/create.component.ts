import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { tap } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { toMessageType } from '../home/home.component';
import { eCapacity } from '../modules/conteiner/models/eCapacity';
import { eOperationType } from '../modules/conteiner/models/eOperationType';
import { ConteinerService } from '../modules/conteiner/services/conteiner.service';

@Component({
  selector: 'app-create-conteiner-component',
  templateUrl: './create.component.html',
  styleUrls:['../shared/scss/base.css']
})
export class CreateConteinerComponent {
  
  constructor(
    private formBuilder: FormBuilder,
    private service: ConteinerService
    ) {

  }
  submitted = false;
  readonly form: FormGroup = this.formBuilder.group({
    number: ['', Validators.compose([Validators.minLength(11), Validators.maxLength(11), Validators.required])],
    capacity: ['', Validators.required],
    operation: ['', [Validators.required]]
  });
  readonly number = this.form.get('number');
  readonly capacity = this.form.get('capacity');
  readonly operation = this.form.get('operation');

  
  sendForm() {
    
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    
    this.service.create(this.form.value)
    .pipe(tap(() => {
      Swal.fire('Inserido com sucesso!', '', 'success');
      this.form.reset();
      this.submitted = false;
    }))
    .subscribe();
  }
  
  operationsType() {
    return [eOperationType.Cabotagem,
            eOperationType.Exportacao,
            eOperationType.Importacao,
            eOperationType.Transbordo];
  }

  typeToMessage(operation: eOperationType){
    return toMessageType(operation);
  }

  capacities() {
    return [eCapacity.TWENTY, eCapacity.FOURTY];
  }
}

