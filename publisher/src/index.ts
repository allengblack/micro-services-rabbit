import express, { Request, Response } from "express"

let app = express()
app.use(express.urlencoded({ extended: true }))
app.use(express.json())

app.get("/", (req: Request, res: Response) => {
    res.json({ status: 200, message: "all is well" })
})

app.listen(4000, () => console.log("Node server started on PORT 4000"))
