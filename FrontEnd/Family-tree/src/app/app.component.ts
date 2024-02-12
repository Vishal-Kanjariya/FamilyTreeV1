import { RouterOutlet } from '@angular/router';
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import * as go from 'gojs';
import { DiagramComponent } from 'gojs-angular';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit, AfterViewInit {
  name = 'Angular';

  @ViewChild(DiagramComponent, { static: false }) diagramComponent!: DiagramComponent;

  public diagramDivClassName = 'myDiagramDiv';
  public diagramModelData = { prop: 'value', color: 'red' };

  public dia: go.Diagram | null = null; // Initialize it as null
  public familyData: any;

  @ViewChild('myDiag', { static: false }) myDiag!: DiagramComponent;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.fetchFamilyData();
  }

  ngAfterViewInit() {
    this.initDiagram();
  }

  fetchFamilyData() {
    this.http.get<any>('api/UpdateFamily/GetDescendants').subscribe(
      (data) => {
        this.familyData = data;
      },
      (error) => {
        console.error('Error fetching family data:', error);
      }
    );
  }

  initDiagram(): go.Diagram {
    const $ = go.GraphObject.make;
    const dia = $(go.Diagram, {
      'toolManager.hoverDelay': 100,
      allowCopy: false,
      layout: $(go.TreeLayout, {
        angle: 90,
        nodeSpacing: 10,
        layerSpacing: 40,
        layerStyle: go.TreeLayout.LayerUniform
      })
    });

    // Define node and link templates

    dia.model = new go.TreeModel(this.familyData);

    this.dia = dia;

    return dia;
  }

  buttonCallback(e: go.InputEvent, obj: go.GraphObject) { // Specify the types for parameters
    console.log('e2: ', e.diagram.model.modelData['color']);
    console.log('this: ', this.name);
  }

  onModelChange($event :any) {
    console.log('Event: ', $event);
  }

  save() {
    const data = this.diagramComponent.diagram.model.toJson();
    console.log('data: ', data);
  }
}