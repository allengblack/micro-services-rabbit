import amqp, { Connection } from "amqplib/callback_api";
import dotenv from "dotenv";

dotenv.config();

class RabbitMqService {
    // @ts-ignore
    connection: Connection;

    constructor(url: string) {
        amqp.connect(url, (error, conn) => {
            if (error) {
                throw error;
            }
            this.connection = conn;
        });
    }

    async publish(queue: string, payload: any) {
        this.connection.createChannel((err, channel) => {
            if (err) {
                throw err;
            }

            channel.assertQueue(queue, { durable: false });

            channel.sendToQueue(queue, Buffer.from(JSON.stringify(payload)));
            console.log(`Sent to queue: ${payload}`);

            channel.close((err) => {
                if (err) throw err;
            });
        });
    }

    closeConnection() {
        this.connection.close();
    }
}

// @ts-ignore
export const RabbitService = new RabbitMqService(process.env.AMQP_URL);
