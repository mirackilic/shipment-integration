import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Order } from '../models/order';
import { ResponseVM } from '../models/responseVM';
import { OrderService } from '../services/order-service';

import { UpdateShipmentComponent } from '../update-shipment/update-shipment.component';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
  orders: Order[] = [];
  editable: boolean = false;
  columnIds: any;
  selectedShipmentId: number = 0;
  selectedStatus: number = 1;
  shipmentStatusType: any = { name: "Sipariş Alındı", id: 1 };
  isUpdateButtonDisable: boolean = true;
  columns: any[] = [
    // { id: 1, prop: "select" },
    { id: 2, prop: "id" },
    { id: 2, prop: "referenceNumber" },
    { id: 3, prop: "fromAddress" },
    { id: 4, prop: "toAddress" },
    { id: 5, prop: "quantity" },
    { id: 6, prop: "quantityUnit" },
    { id: 7, prop: "weight" },
    { id: 8, prop: "weightType" },
    { id: 9, prop: "materialCode" },
    { id: 10, prop: "materialName" },
    { id: 11, prop: "note" },
    { id: 12, prop: "status" },
  ];

  shipmentStatusTypes = [
    { name: "Sipariş Alındı", id: 1 },
    { name: "Yola Çıktı", id: 2 },
    { name: "Dağıtım Merkezinde", id: 3 },
    { name: "Dağıtıma Çıktı", id: 4 },
    { name: "Teslim Edildi", id: 5 },
    { name: "Teslim Edilemedi", id: 6 }
  ];

  constructor(private orderService: OrderService,
    // public dialog: MatDialog,
    // public dialogRef: MatDialogRef<UpdateShipmentComponent>,
  ) { }

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.orderService.getOrders().subscribe((response) => {
      this.orders = <Order[]>response;

      this.orders.map(x => { })
      this.columnIds = this.columns.map((x) => x.prop);

    });
  }

  onShipmentSelect(event: any) {
    this.orders.map(x => x.showStatusSelect = false);
    event.showStatusSelect = true;
    this.selectedStatus = event.status;
    this.selectedShipmentId = event.id;
  }

  UpdateShipmentStatus() {
    if (this.selectedShipmentId <= 0) {
      alert("Sipariş Seçimi Yapılmadı!");
      return;
    }

    this.orderService.updateOrder(this.selectedShipmentId, this.selectedStatus).subscribe((res) => {
      var response = <ResponseVM>res;
      if (response.isSuccess) {
        this.selectedShipmentId = 0;
        this.getOrders();
        alert(response.id + " numaralı sipariş başarıyla güncellendi");
      }
      else {
        alert(response.id + " numaralı sipariş güncellenirken bir sorun oluştu" + response.errorMessage);
      }
    })
  }

  setSelection(event: any) {
    this.selectedStatus = event.value;
  }

  handleShipmentStatus(status: number) {
    switch (status) {
      case 1:
        return "Sipariş Alındı";
        break;
      case 2:
        return "Yola Çıktı";
        break;
      case 3:
        return "Dağıtım Merkezinde";
        break;
      case 4:
        return "Dağıtıma Çıktı";
        break;
      case 5:
        return "Teslim Edildi";
        break;
      case 6:
        return "Teslim Edilemedi";
        break;

      default:
        return "Bilinmiyor"
        break;
    }
  }
}
