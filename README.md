# flow
Contains the simplic flow system


---



## Event nodes

### OnDocumentScanned

__Type:__ Event node
__Args__ 
*Type*: ScannedDocument
*Properties*:
- *Blob*: byte[]
- *Barcode*: string


__FlowOut__ 
FlowOut: `ActionNode`

__DataPin - Out__
*Document:* ScannedDocument
*Blob:* byte[]
*Barcode*: string

## Action nodes
