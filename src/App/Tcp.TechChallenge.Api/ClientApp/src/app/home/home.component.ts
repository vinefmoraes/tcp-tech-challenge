import { Component } from '@angular/core';
import { ConteinerService } from '../modules/conteiner/services/conteiner.service';
import { Conteiner } from '../modules/conteiner/models/conteiner';
import { eOperationType } from '../modules/conteiner/models/eOperationType';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  //styleUrls:['../shared/scss/base.scss']
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
}

export function toMessageType(type: eOperationType): string {
  switch (type) {
    case eOperationType.Cabotagem: return 'Cabotagem';
    case eOperationType.Exportacao: return 'Exportação';
    case eOperationType.Importacao: return 'Importação';
    case eOperationType.Transbordo: return 'Transbordo';
  }
}
