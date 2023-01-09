import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class OrderService {

    constructor(private _httpClient: HttpClient) {
    }

    getOrders() {
        return this._httpClient.get("https://localhost:7274/Shipment")
    }

    updateOrder(id: number, status: number) {
        var requestModel = {
            id: id,
            status: status
        }

        return this._httpClient.put("https://localhost:7274/Shipment", requestModel)
    }
}