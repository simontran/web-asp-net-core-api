package jp.co.suntac.data.source.remote

import android.app.Service
import android.content.Intent
import android.os.IBinder
import com.microsoft.signalr.HubConnectionBuilder


class SignalRService : Service() {
    companion object {
        const val URL = "url_example"
    }

    val hubConnection = HubConnectionBuilder.create(URL)
        .build()

    override fun onBind(intent: Intent): IBinder {
        TODO("Return the communication channel to the service.")
    }

    override fun onStartCommand(intent: Intent?, flags: Int, startId: Int): Int {
        hubConnection.on(
            "Send",
            { message: String -> println("New Message: $message") },
            String::class.java
        )
        hubConnection.start().blockingAwait();
        return super.onStartCommand(intent, flags, startId)
    }

    private fun send(content: String) {
        hubConnection.send("Send", content)
    }

    override fun onDestroy() {
        hubConnection.stop();
        super.onDestroy()

    }
}