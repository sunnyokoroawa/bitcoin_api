using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class ListpeersRequest
    {
    }


    public class ListpeersResponse
    {
        public ListpeersResponse()
        {
            peers = new List<ListpeersResponsePeer>();
        }

        public List<ListpeersResponsePeer> peers { get; set; }
    }


    public class ListpeersResponsePeer
    {
        public ListpeersResponsePeer()
        {
            channels = new List<ListpeersResponsePeerChannel>();
            netaddr = new List<string>();
        }

        public string id { get; set; }
        public bool connected { get; set; }
        public List<string> netaddr { get; set; }
        public string features { get; set; }
        public List<ListpeersResponsePeerChannel> channels { get; set; }
    }

    public class ListpeersResponsePeerChannel
    {
        public ListpeersResponsePeerChannel()
        {
            features = new List<string>();
            status = new List<string>();
        }

        public string state { get; set; }
        public string scratch_txid { get; set; }
        public string last_tx_fee_msat { get; set; }
        public ListpeersResponsePeerFeerate feerate { get; set; }
        public string owner { get; set; }
        public string short_channel_id { get; set; }
        public int direction { get; set; }
        public string channel_id { get; set; }
        public string funding_txid { get; set; }
        public int funding_outnum { get; set; }
        public string close_to_addr { get; set; }
        public string close_to { get; set; }
        public bool _private { get; set; }
        public string opener { get; set; }
        public List<string> features { get; set; }
        public ListpeersResponsePeerFunding funding { get; set; }
        public int msatoshi_to_us { get; set; }
        public string to_us_msat { get; set; }
        public int msatoshi_to_us_min { get; set; }
        public string min_to_us_msat { get; set; }
        public long msatoshi_to_us_max { get; set; }
        public string max_to_us_msat { get; set; }
        public int msatoshi_total { get; set; }
        public string total_msat { get; set; }
        public string fee_base_msat { get; set; }
        public int fee_proportional_millionths { get; set; }
        public int dust_limit_satoshis { get; set; }
        public string dust_limit_msat { get; set; }
        public ulong max_htlc_value_in_flight_msat { get; set; }
        public string max_total_htlc_in_msat { get; set; }
        public int their_channel_reserve_satoshis { get; set; }
        public string their_reserve_msat { get; set; }
        public int our_channel_reserve_satoshis { get; set; }
        public string our_reserve_msat { get; set; }
        public int spendable_msatoshi { get; set; }
        public string spendable_msat { get; set; }
        public int receivable_msatoshi { get; set; }
        public string receivable_msat { get; set; }
        public int htlc_minimum_msat { get; set; }
        public string minimum_htlc_in_msat { get; set; }
        public string minimum_htlc_out_msat { get; set; }
        public string maximum_htlc_out_msat { get; set; }
        public int their_to_self_delay { get; set; }
        public long our_to_self_delay { get; set; }
        public long max_accepted_htlcs { get; set; }
        public ListpeersResponsePeerState_Changes[] state_changes { get; set; }
        public List<string> status { get; set; }
        public int in_payments_offered { get; set; }
        public int in_msatoshi_offered { get; set; }
        public string in_offered_msat { get; set; }
        public int in_payments_fulfilled { get; set; }
        public int in_msatoshi_fulfilled { get; set; }
        public string in_fulfilled_msat { get; set; }
        public int out_payments_offered { get; set; }
        public int out_msatoshi_offered { get; set; }
        public string out_offered_msat { get; set; }
        public int out_payments_fulfilled { get; set; }
        public int out_msatoshi_fulfilled { get; set; }
        public string out_fulfilled_msat { get; set; }
        public object[] htlcs { get; set; }
    }

    public class ListpeersResponsePeerFeerate
    {
        public int perkw { get; set; }
        public int perkb { get; set; }
    }

    public class ListpeersResponsePeerFunding
    {
        public string local_msat { get; set; }
        public string remote_msat { get; set; }
        public string pushed_msat { get; set; }
    }

    public class ListpeersResponsePeerState_Changes
    {
        public DateTime timestamp { get; set; }
        public string old_state { get; set; }
        public string new_state { get; set; }
        public string cause { get; set; }
        public string message { get; set; }
    }

    //public class ListpeersResponsePeer
    //{
    //    public ListpeersResponsePeer()
    //    {
    //        netaddr = new List<string>();
    //    }

    //    public string id { get; set; }
    //    public bool connected { get; set; }
    //    public List<string> netaddr { get; set; }
    //    public string features { get; set; }
    //    public object[] channels { get; set; }
    //}


}
