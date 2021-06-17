import { Component } from '@angular/core';
import { ConteinerService } from '../modules/conteiner/services/conteiner.service';
import { Conteiner } from '../modules/conteiner/models/conteiner';
import { eOperationType } from '../modules/conteiner/models/eOperationType';
import Swal from 'sweetalert2';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls:['../shared/scss/base.css']
})
export class HomeComponent {
  conteiners: Conteiner[];
  constructor(public service: ConteinerService) {
    service
      .findAllConteiners()
      .subscribe(result => this.conteiners = result);
  }

  toMessageType(type: eOperationType): string {
    switch (type) {
      case eOperationType.Cabotagem: return 'Cabotagem';
      case eOperationType.Exportacao: return 'Exportação';
      case eOperationType.Importacao: return 'Importação';
      case eOperationType.Transbordo: return 'Transbordo';
    }
  }

  confirmDelete(conteiner){
    Swal.fire({
      title: `Tem certeza que deseja excluir o conteiner ${conteiner.number}?`,
      showDenyButton: true,
      showCancelButton: true,
      confirmButtonText: `Sim`,
      denyButtonText: `Não`,
      icon: 'warning',
    }).then((result) => {
      if (result.isConfirmed) {
        this.service.delete(conteiner.number)
        .pipe(
          tap(()=>Swal.fire('Conteiner removido com sucesso!', '', 'success')),
          tap(() => this.conteiners.splice(this.conteiners.indexOf(conteiner), 1))
        ).subscribe()
      }
    })
  }
}

export function toMessageType(type: eOperationType): string {
  switch (type) {
    case eOperationType.Cabotagem: return 'Cabotagem';
    case eOperationType.Exportacao: return 'Exportação';
    case eOperationType.Importacao: return 'Importação';
    case eOperationType.Transbordo: return 'Transbordo';
  }
}
