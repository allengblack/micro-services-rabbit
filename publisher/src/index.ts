import express, { Request, Response } from "express";
import dotenv from "dotenv";
import { RabbitService } from "./services/rabbit.service";
import morgan from "morgan";

dotenv.config();

let app = express();
//@ts-ignore
app.use(express.urlencoded({ extended: true }));
//@ts-ignore
app.use(express.json());
//@ts-ignore
app.use(morgan("tiny"));

app.get("/", (req: Request, res: Response) => {
    res.json({ status: 200, message: "all is well" });
});

app.post("/api/people", async (req: Request, res: Response) => {
    // @ts-ignore
    const body = req.body;
    await RabbitService.publish("new_person", body);

    res.json({ status: 200, message: "Queued successfully" });
});

app.listen(process.env.PORT, () =>
    console.log(`Node server started on PORT ${process.env.PORT}`),
);
