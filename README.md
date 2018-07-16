# flow
Contains the simplic flow system


---



## Event nodes

### OnDocumentScanned

__Type:__ EventNode

__Args:__ `ScannedDocument`

*Properties*:
- *Blob*: `byte[]`
- *Barcode*: `string`


#### FlowOut

FlowOut: `ActionNode`

#### DataPin out

- *Document:* ScannedDocument
- *Blob:* `byte[]`
- *Barcode*: `string`

## Action nodes
