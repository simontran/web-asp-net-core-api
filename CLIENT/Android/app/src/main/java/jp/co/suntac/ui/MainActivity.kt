package jp.co.suntac.ui

import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.activity.ComponentActivity
import androidx.lifecycle.lifecycleScope
import com.microsoft.signalr.HubConnection
import com.microsoft.signalr.HubConnectionBuilder
import com.microsoft.signalr.HubConnectionState
import com.microsoft.signalr.TypeReference
import jp.co.suntac.R
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.lang.reflect.Type


class MainActivity : ComponentActivity() {
    private lateinit var edtUrl: EditText
    private lateinit var text: TextView
    private lateinit var btnConnect: Button
    private var hubConnection: HubConnection? = null
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.main_activity)
        edtUrl = findViewById(R.id.edtUrl)
        btnConnect = findViewById(R.id.btnConnect)
        text = findViewById(R.id.tvContent)
        hubConnection = HubConnectionBuilder.create(edtUrl.text.toString())
            .build()
        val type: Type = object : TypeReference<List<String>>() {}.type
        hubConnection?.on(
            "SendOffersToUser",
            { list: List<String> ->
                lifecycleScope.launch(Dispatchers.Main) {
                    list.forEach { content ->
                        text.append("$content \n")
                    }
                }
            },
            type,
        )

        btnConnect.setOnClickListener {
            if (hubConnection?.connectionState == HubConnectionState.DISCONNECTED) {
                hubConnection?.start()
                btnConnect.text = "Connected"
                Toast.makeText(this, "connected", Toast.LENGTH_SHORT).show()
            } else if (hubConnection?.connectionState == HubConnectionState.CONNECTED) {
                hubConnection?.stop()
                btnConnect.text = "Disconnected"
                Toast.makeText(this, "disconnected", Toast.LENGTH_SHORT).show()
            }


        }

        edtUrl.setOnClickListener {
            Toast.makeText(this, "${hubConnection?.connectionState}", Toast.LENGTH_SHORT).show()
        }
    }

}