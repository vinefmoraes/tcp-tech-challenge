import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
import { toMessageType } from '../home/home.component';
import { Conteiner } from '../modules/conteiner/models/conteiner';
import { eCapacity } from '../modules/conteiner/models/eCapacity';
import { eOperationType } from '../modules/conteiner/models/eOperationType';
import { ConteinerService } from '../modules/conteiner/services/conteiner.service';

@Component({
  selector: 'app-edit-conteiner-component',
  templateUrl: './edit.component.html',
  styleUrls:['../shared/scss/base.css']
})
export class EditConteinerComponent implements OnInit {

  conteinerEdit: Conteiner;
  constructor(
    private formBuilder: FormBuilder,
    private service: ConteinerService,
    private route: ActivatedRoute
  ) {

  }

  form: FormGroup;
  number: AbstractControl;
  capacity: AbstractControl;
  operation: AbstractControl;

  ngOnInit(): void {
    let identifier = this.route.snapshot.paramMap.get('number');
    this.service.findConteinerByIdentifier(identifier)
      .subscribe(response => {
        this.conteinerEdit = response;
        this.form = this.formBuilder.group({
          number: [{ value: this.conteinerEdit.number, disabled: true }, Validators.compose([Validators.minLength(11), Validators.maxLength(11), Validators.required])],
          capacity: [this.conteinerEdit.capacity, Validators.required],
          operation: [this.conteinerEdit.operation, [Validators.required]]
        });

        this.number = this.form.get('number');
        this.capacity = this.form.get('capacity');
        this.operation = this.form.get('operation');
      })
  }

  sendForm() {
    if (this.form.invalid) {
      return;
    }

    this.service.edit(
      this.number.value, 
      { ...this.form.value, number: this.number.value })
    .subscribe(() => Swal.fire('Conteiner alterado com sucesso!', '', 'success'));
  }

  operationsType() {
    return [eOperationType.Cabotagem,
    eOperationType.Exportacao,
    eOperationType.Importacao,
    eOperationType.Transbordo];
  }

  typeToMessage(operation: eOperationType) {
    return toMessageType(operation);
  }

  capacities() {
    return [eCapacity.TWENTY, eCapacity.FOURTY];
  }
}

